using ShoppingCartApi.Model;
using ShoppingCartApi.Repositories.Abstract;

namespace ShoppingCartApi.Repositories.Concrete
{
    public class OrderRepository : Repository<OrderModel>
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
