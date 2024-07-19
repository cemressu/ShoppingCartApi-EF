using Microsoft.AspNetCore.Mvc;
using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;
using ShoppingCartApi.Service.CustomerService;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }



        [HttpGet("GetAllCustomers")]
        public async Task<IEnumerable<CustomerModel>> GetAllBaskets()
        {
            IEnumerable<CustomerModel> customers = await _customerService.GetAllCustomer();

            return customers;
        }


        [HttpGet("{id}/GetCustomerById")]
        public async Task<ActionResult<CustomerModel>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound($"Customer ID = {id} not found");
            }
            return Ok(customer);
        }


        [HttpPost("AddCustomer")]
        public async Task<ActionResult<BasketModel>> AddCustomer([FromBody] CustomerModel customerModel)
        {
            await _customerService.AddCustomer(customerModel);
            return Ok("Customer added successfully");
        }


        [HttpPut("{id}/UpdateCustomer")]
        public async Task<ActionResult<string>> UpdateCustomer(int id, [FromBody] CustomerModel customerModel)
        {
            var result = await _customerService.UpdateCustomer(id, customerModel);
            if (result == "Customer not found")
            {
                return NotFound($"Customer ID = {id} not found");
            }
            return Ok("Customer updated successfully");
        }



        [HttpDelete("{id}/DeleteCustomer")]
        public async Task<ActionResult<string>> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomer(id);
            if (result == null)
            {
                return NotFound($"Customer ID = {id} not found");
            }
            return Ok("Customer deleted successfully");
        }

    }
}
