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
    }
}
