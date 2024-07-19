using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;

namespace ShoppingCartApi.Repositories.Concrete
{
    public class CustomerRepository: Repository<CustomerModel>
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
