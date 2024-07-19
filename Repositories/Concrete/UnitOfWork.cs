using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;
using ShoppingCartApi.Repositories.Abstract;

namespace ShoppingCartApi.Repositories.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IRepository<BasketModel> BasketRepository { get; set; }
        public IRepository<CustomerModel> CustomerRepository { get; set; }
        public IRepository<OrderModel> OrderRepository { get; set; }
        public IRepository<ProductModel> ProductRepository { get; set; }



        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            BasketRepository = new BasketRepository(_appDbContext);
            CustomerRepository = new CustomerRepository(_appDbContext);
            ProductRepository = new ProductRepository(_appDbContext);
            OrderRepository = new OrderRepository(_appDbContext);
        }

    }
}
