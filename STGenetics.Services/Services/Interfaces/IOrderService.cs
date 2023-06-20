using STGenetics.Application.Models.Order;

namespace STGenetics.Application.Services.Interfaces
{
    public interface IOrderService
    {

        /// <summary>
        /// Process the order to buy bulls and cows in the Api
        /// </summary>
        /// <param name="orderIn"></param>
        /// <returns>Thr order id, total price an a mesage if there is any problem</returns>
        public Task<(OrderOut, string)> ProcessOrder(OrderIn orderIn);
    }
}
