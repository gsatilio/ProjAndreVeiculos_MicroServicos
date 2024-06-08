using Models;
using Repositories;

namespace Services
{
    public class CustomerService
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
    }
}
