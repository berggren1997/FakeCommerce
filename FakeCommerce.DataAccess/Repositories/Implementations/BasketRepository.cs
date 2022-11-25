using FakeCommerce.DataAccess.Data;
using FakeCommerce.DataAccess.Repositories.Contracts;
using FakeCommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeCommerce.DataAccess.Repositories.Implementations
{
    public class BasketRepository : RepositoryBase<Basket>, IBasketRepository
    {
        public BasketRepository(CommerceDbContext context) : base(context)
        { }

        public async Task<Basket?> GetBasket(string buyerId, bool trackChanges) =>
            await FindByCondition(x => x.BuyerId == buyerId, trackChanges)
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync();
    }
}
