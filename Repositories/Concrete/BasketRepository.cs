using PostrgreSqlApi.Model;
using ShoppingCartApi.Repositories.Abstract;

namespace ShoppingCartApi.Repositories.Concrete
{
    public class BasketRepository : Repository<BasketModel>
    {
        private readonly AppDbContext _dbContext;

        public BasketRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


  


    }
}
