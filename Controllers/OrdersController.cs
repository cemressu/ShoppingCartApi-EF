using Microsoft.AspNetCore.Mvc;
using PostrgreSqlApi.Model;


namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public OrdersController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //dependency injection uygulandı!!!!



        
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            var orders = _dbContext.GetAllOrders();

            return Ok(orders);
        }
        //tüm siparişleri alır ve listeler



       
        [HttpGet("{id}/GetOrderById")]
        public IActionResult GetOrderById(int id)
        {
            var order = _dbContext.GetOrderById(id);

            if (order==null)
            {
                return NotFound("Order not found.");
            }
            return Ok(order);
        }
        //belirtilen id'ye sahip ürünün bilgilerini verir


        
        [HttpPost("AddOrder")]
        public IActionResult AddOrder([FromBody] OrdersModel orders)
        {
            _dbContext.AddOrder(orders);

            return Ok("Order added successfully.");
        }
        //yeni sipariş ekler



        
        [HttpPut("{id}/UpdateOrder")]
        public IActionResult UpdateOrder(int id, [FromBody] OrdersModel orders)
        {
            var updated = _dbContext.UpdateOrder(id, orders);

            if (updated == null || updated == "0")
            {
                return NotFound("Order not found.");
            }
            return Ok("Order updated succesfully.");
        }
        //belirtilen id deki siparişi günceller



      
        [HttpDelete("{id}/DeleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            var deleted = _dbContext.DeleteOrder(id);

            if (deleted == null || deleted == "0")
            {
                return NotFound("Orders not found.");
            }
            return Ok("Order deleted succesfully.");
        }
        //belirtilen id deki siparişi siler
    }
}

