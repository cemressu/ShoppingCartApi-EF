using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;

namespace ShoppingCartApi.Service.CustomerService
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerModel>> GetAllCustomer();
        Task<CustomerModel> GetCustomerById(int id);
        Task<string> AddCustomer(CustomerModel customerModel);
        Task<string> UpdateCustomer(int id, CustomerModel customerModel);
        Task<string> DeleteCustomer(int id);
    }
}
