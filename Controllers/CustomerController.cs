using Models;
using Services;

namespace Controllers
{
    public class CustomerController
    {
        private CustomerService _service = new();

        public CustomerController()
        {

        }
        public int Insert(Customer customer, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(customer, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
        public async Task<List<Customer>> GetAll(int type)
        {
            List<Customer> list = new List<Customer>();
            try
            {
                list = await _service.GetAll(type);
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
                list = await _service.Get(document, type);
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
