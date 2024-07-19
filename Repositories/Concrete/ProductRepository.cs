using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;

namespace ShoppingCartApi.Repositories.Concrete
{
    public class ProductRepository : Repository<ProductModel>
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
