using Microsoft.Extensions.Options;
using STGenetics.Application.Models.Order;
using STGenetics.Application.Ports;
using STGenetics.Application.Services.Interfaces;
using STGenetics.Application.Tools.Settings;
using STGenetics.Domain.BusinessRules;
using STGenetics.Domain.Entities;
using STGenetics.Domain.Tools;
namespace STGenetics.Application.Services
{
   
    public class OrderService : IOrderService
    {
        private readonly IAnimalService _animalService;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderSettings _orderSettings;
        public OrderService(IAnimalService animalService, IOptions<OrderSettings> orderSettings, 
            IOrderRepository orderRepository)
        {
            _animalService = animalService;
            this._orderSettings = orderSettings.Value;
            _orderRepository = orderRepository;
        }


        public async Task<(OrderOut, string)> ProcessOrder(OrderIn orderIn)
        {
            var OrderId = 0;
            try
            {           
                var orderOut = new OrderOut();

                if(!VerifyOrderIn(orderIn))return (orderOut, "Validations problem");          

                if (VerifyDuplicates(orderIn.AnimalsIds))  return (orderOut,_orderSettings.DuplicateMessage);


                var transaction = new TransactionToOrder()
                {
                    ClientName = orderIn.ClientName,
                    PurchaseDate = DateTime.Now,
                    Freigth = _orderSettings.FreightCost,
                    Discounts = new List<string>(),
                    Animals = await GetAnimalsInfoAsync(orderIn.AnimalsIds),
                    Total = 0,
                };

                if (transaction.Animals.Count != orderIn.AnimalsIds.Count) 
                {
                    var list = orderIn.AnimalsIds.Except(transaction.Animals.Select(c => c.AnimalId)).ToList();

                    return (orderOut, "Animals listed are not active for selling : " + string.Join(" ,", list));
                }

                transaction = ImplementingBusinessRules(transaction);

                await _animalService.UpdateAnimalsStateAsync(orderIn.AnimalsIds, false);

                var OrderEntity = BuildOrder(transaction);

                OrderId = await _orderRepository.SaveOrder(OrderEntity, orderIn.AnimalsIds);

                orderOut.OrderId = OrderId;
                orderOut.PurchaseAmount = OrderEntity.Total;

                return (orderOut,"");

            }
            catch (Exception)
            {

                //we must apply a safe rollback
                await _animalService.UpdateAnimalsStateAsync(orderIn.AnimalsIds, true);

                if(OrderId > 0)  await _orderRepository.InactiveOrder(OrderId);

                throw;
            }
        }


        private static Order BuildOrder(TransactionToOrder toOrder) 
        {
            return new Order()
            {
                ClientName = toOrder.ClientName,
                Freight = toOrder.Freigth,
                Paid = false,
                PurchaseDate= toOrder.PurchaseDate,
                DiscountsApplied = string.Join(" ,", toOrder.Discounts),
                Total = toOrder.Total
            };
        }

        private async Task<List<Animal>> GetAnimalsInfoAsync(List<int> ids) 
        {
            return await _animalService.GetAnimalsInfoAsync(ids);
        }



        private TransactionToOrder ImplementingBusinessRules(TransactionToOrder toOrder) 
        {
            toOrder = STGeneticsRules.ApplyQuantityDiscount(toOrder);

            toOrder = STGeneticsRules.Apply200AnimalsDiscount(toOrder);

            if (!STGeneticsRules.Apply300AnimalsDiscount(toOrder)) 
            {
                toOrder.Total += _orderSettings.FreightCost;
                toOrder.Freigth = _orderSettings.FreightCost;
                toOrder.Discounts.Add("Buy>300, Freight for free;");
            }
            return toOrder;
        }


        private static bool VerifyOrderIn(OrderIn orderIn) {

            orderIn.ThrowIfNull();

            if(string.IsNullOrEmpty(orderIn.ClientName)) return false;

            if(orderIn.AnimalsIds.Count == 0 ) return false;

            return true;
        }


        private static bool VerifyDuplicates(List<int> ids)
        {
            return ids.GroupBy(x => x).Any(g => g.Count() > 1);
        }

    }
}
