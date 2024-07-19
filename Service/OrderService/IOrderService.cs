using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;

namespace ShoppingCartApi.Service.OrderService
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> GetAllOrders();
        Task<OrderModel> GetOrderById(int id);
        Task<string> AddOrder(OrderModel orderModel);
        Task<string> UpdateOrder(int id, OrderModel orderModel);
        Task<string> DeleteOrder(int id);
    }
}
