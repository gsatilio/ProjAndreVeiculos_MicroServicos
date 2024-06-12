using Models;
using Services;

namespace Controllers
{
    public class AddressController
    {
        private AddressService _service = new();

        public AddressController()
        {

        }
        public int Insert(Address address, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(address, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
        public async Task<List<Address>> GetAll(int type)
        {
            List<Address> list = new List<Address>();
            try
            {
                list = await _service.GetAll(type);
            }
            catch
            {
                throw;
            }
            return list;
        }
        public async Task<Address> Get(int id, int type)
        {
            Address list = new Address();
            try
            {
                list = await _service.Get(id, type);
            }
            catch
            {
                throw;
            }
            return list;
        }
    }
}