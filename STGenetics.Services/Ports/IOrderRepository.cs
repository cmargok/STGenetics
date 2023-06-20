using STGenetics.Domain.Entities;

namespace STGenetics.Application.Ports
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Save and order into the database
        /// </summary>
        /// <param name="order"></param>
        /// <param name="Ids"></param>
        /// <returns>Order Id</returns>
        public Task<int> SaveOrder(Order order, List<int> Ids);


        /// <summary>
        /// Inactives an order when a problem has ocurred
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Confirmation</returns>
        public Task<bool> InactiveOrder(int Id);
    }

}
