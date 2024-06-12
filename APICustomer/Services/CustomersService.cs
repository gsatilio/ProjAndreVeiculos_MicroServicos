using Models;
using Repositories;

namespace APICustomer.Services
{
    public class CustomersService
    {
        private CustomerRepository _repository = new();

        public int Insert(Customer customer, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(customer, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
        public async Task<List<Customer>> GetAll(int type)
        {
            List<Customer> list = new List<Customer>();
            try
            {
                list = await _repository.GetAll(type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return list;
        }
        public async Task<Customer> Get(string document, int type)
        {
            Customer list = new Customer();
            try
            {
                list = await _repository.Get(document, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return list;
        }
    }
}
