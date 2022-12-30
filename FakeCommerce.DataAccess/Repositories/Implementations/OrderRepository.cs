using FakeCommerce.DataAccess.Data;
using FakeCommerce.DataAccess.Repositories.Contracts;
using FakeCommerce.Entities.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace FakeCommerce.DataAccess.Repositories.Implementations
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(CommerceDbContext context) : base(context)
        { }

        public void CreateOrder(Order order) =>
            Create(order);

        public async Task<IEnumerable<Order>> GetUserOrders(string username, bool trackChanges) =>
            await FindByCondition(x => x.BuyerId == username, trackChanges).ToListAsync();
    }
}
