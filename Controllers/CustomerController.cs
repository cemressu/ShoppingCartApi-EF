using Microsoft.AspNetCore.Mvc;
using PostrgreSqlApi.Model;


namespace ShoppingCartApi.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly DbContext _dbContext; 

        public CustomerController(DbContext dbContext) 
        {
            _dbContext = dbContext;    
        }
        // DEPENDENCY INJECTION orneği



        
        [HttpGet ("GetAllCustomers")]
        public  IActionResult GetAllCustomers()
        {
            var customers = _dbContext.GetAllCustomers(); //tüm http metodlarında da _connection yazıyordu 

            return Ok(customers);
        }
        // GET api tüm müşteri kayıtlarını alır 



        
        [HttpGet("{id}/GetCustomerById")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _dbContext.GetCustomerById(id);

            if (customer==null)
            {
                return NotFound("Customer not found.");
            }
            return Ok(customer);
        }
        //belirtilen id'ye sahip müşteri kaydını verir



       
        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer([FromBody] CustomerModel customer)
        {
            _dbContext.AddCustomer(customer);

            return Ok("Customer added successfully.");
        }
        // POST api yeni müşteri ekler ve işlem başarılıysa "customer added successfully" mesajını ekrana yazar



       
        [HttpPut("{id}/UpdateCustomer")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerModel customer)
        {
            var updated = _dbContext.UpdateCustomer(id, customer);

            if (updated == null || updated == "0")
            {
                return NotFound("Customer not found."); //güncelleme basarisiz olursa bunu
            }
            return Ok("Customer updated succesfully."); //basarili olursa bunu döndürür
                                         //istenilen id deki bir sepeti güncellemek için
        }
        // PUT api istenilen müşterinin id sini alır ve o id deki müşteri bilgilerini günceller



       
        [HttpDelete("{id}/DeleteCustomer")]
        public IActionResult DeleteCustomer(int id)
        {
            var deleted = _dbContext.DeleteCustomer(id);

            if (deleted==null || deleted == "0")
            {
                return NotFound("Customer not found.");
            }
            return Ok("Customer deleted successfully.");
        }
        // DELETE api belirtilen id deki müşteriyi siler 
    }
}

