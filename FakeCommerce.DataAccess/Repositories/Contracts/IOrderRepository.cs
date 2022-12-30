using FakeCommerce.Entities.Models.OrderAggregate;

namespace FakeCommerce.DataAccess.Repositories.Contracts
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Gets orders that belongs to a specifc user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetUserOrders(string username, bool trackChanges);
        void CreateOrder(Order order);
    }
}
