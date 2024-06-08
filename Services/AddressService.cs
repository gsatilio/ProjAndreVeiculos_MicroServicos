using Models;
using Repositories;

namespace Services
{
    public class AddressService
    {
        private AddressRepository _repository = new();

        public int Insert(Address address, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(address, type);
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
