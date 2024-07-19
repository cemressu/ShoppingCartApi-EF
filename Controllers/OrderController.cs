using Microsoft.AspNetCore.Mvc;
using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;
using ShoppingCartApi.Service.BasketService;
using ShoppingCartApi.Service.OrderService;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }




        [HttpGet("GetAllOrders")]
        public async Task<IEnumerable<OrderModel>> GetAllOrders()
        {
            IEnumerable<OrderModel> orders = await _orderService.GetAllOrders();

            return orders;
        }


        [HttpGet("{id}/GetOrderById")]
        public async Task<ActionResult<OrderModel>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound($"Order ID = {id} not found");
            }
            return Ok(order);
        }


        [HttpPost("AddOrder")]
        public async Task<ActionResult<OrderModel>> AddOrder([FromBody] OrderModel orderModel)
        {
            await _orderService.AddOrder(orderModel);
            return Ok("Order added successfully");
        }



        [HttpPut("{id}/UpdateOrder")]
        public async Task<ActionResult<string>> UpdateOrder(int id, [FromBody] OrderModel orderModel)
        {
            var result = await _orderService.UpdateOrder(id, orderModel);
            if (result == "Order not found")
            {
                return NotFound($"Order ID = {id} not found");
            }
            return Ok("Order updated successfully");
        }



        [HttpDelete("{id}/DeleteOrder")]
        public async Task<ActionResult<string>> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (result == null)
            {
                return NotFound($"Order ID = {id} not found");
            }
            return Ok("Order deleted successfully");
        }
    }
}
