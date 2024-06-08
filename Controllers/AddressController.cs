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
    }
}
