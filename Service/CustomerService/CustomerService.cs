using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;
using ShoppingCartApi.Repositories.Abstract;

namespace ShoppingCartApi.Service.CustomerService
{
    public class CustomerService: ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<CustomerModel>> GetAllCustomer()
        {
            return await _unitOfWork.CustomerRepository.GetAll();
        }



        public async Task<CustomerModel> GetCustomerById(int id)
        {
            return await _unitOfWork.CustomerRepository.GetById(id);
        }



        public async Task<string> AddCustomer(CustomerModel customerModel)
        {
            await _unitOfWork.CustomerRepository.Add(customerModel);
            return "Basket added successfully";
        }


        public async Task<string> UpdateCustomer(int id, CustomerModel customerModel)
        {
            var existingCustomer = await _unitOfWork.CustomerRepository.GetById(id);
            if (existingCustomer == null)
            {
                return "Customer not found";
            }
            existingCustomer.Name = customerModel.Name;
            existingCustomer.Surname = customerModel.Surname;
            existingCustomer.PhoneNumber = customerModel.PhoneNumber;
            existingCustomer.Address = customerModel.Address;
            existingCustomer.Email = customerModel.Email;

            await _unitOfWork.CustomerRepository.Update(existingCustomer);
            return "Customer updated successfully";
        }



        public async Task<string> DeleteCustomer(int id)
        {
            var result = await _unitOfWork.CustomerRepository.GetById(id);

            if (result == null)
            {
                return "Customer not found";
            }
            await _unitOfWork.CustomerRepository.Delete(id);
            return "Customer deleted successfully";
        }

     

       
    }
}
