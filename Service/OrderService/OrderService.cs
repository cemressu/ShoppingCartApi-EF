using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;
using ShoppingCartApi.Repositories.Abstract;

namespace ShoppingCartApi.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<IEnumerable<OrderModel>> GetAllOrders()
        {
            return await _unitOfWork.OrderRepository.GetAll();
        }



        public async Task<OrderModel> GetOrderById(int id)
        {
            return await _unitOfWork.OrderRepository.GetById(id);
        }



        public async Task<string> AddOrder(OrderModel orderModel)
        {
            await _unitOfWork.OrderRepository.Add(orderModel);
            return "Order added successfully";
        }



        public async Task<string> UpdateOrder(int id, OrderModel orderModel)
        {
            var existingOrder = await _unitOfWork.OrderRepository.GetById(id);
            if (existingOrder == null)
            {
                return "Order not found";
            }
            existingOrder.CustomerID = orderModel.CustomerID;
            existingOrder.ProductID = orderModel.ProductID;
            existingOrder.OrderDate = orderModel.OrderDate;
            existingOrder.TotalPrice = orderModel.TotalPrice;
            existingOrder.Status = orderModel.Status;
            existingOrder.CargoCompany = orderModel.CargoCompany;
            existingOrder.Quantity = orderModel.Quantity;


            await _unitOfWork.OrderRepository.Update(existingOrder);
            return "Order updated successfully";
        }



        public async Task<string> DeleteOrder(int id)
        {
            var result = await _unitOfWork.OrderRepository.GetById(id);

            if (result == null)
            {
                return "Order not found";
            }
            await _unitOfWork.OrderRepository.Delete(id);
            return "Order deleted successfully";
        }

       

    }
}
