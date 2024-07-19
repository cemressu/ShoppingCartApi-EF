using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;
using ShoppingCartApi.Repositories.Abstract;
using ShoppingCartApi.Repositories.Concrete;

namespace ShoppingCartApi.Repositories.Abstract
{
    public interface IUnitOfWork
    {

        IRepository<BasketModel> BasketRepository { get; set; }
        IRepository<CustomerModel> CustomerRepository { get; set; }
        IRepository<ProductModel> ProductRepository { get; set; }
        IRepository<OrderModel> OrderRepository { get; set; }

    }
}
