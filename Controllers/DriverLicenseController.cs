using Models;
using Services;

namespace Controllers
{
    public class DriverLicenseController
    {
        private DriverLicenseService _service = new();

        public DriverLicenseController()
        {

        }
        public async Task<long> Insert(DriverLicense cnh, int type)
        {
            long result = 0;
            try
            {
                result = await _service.Insert(cnh, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
        public async Task<List<DriverLicense>> GetAll(int type)
        {
            List<DriverLicense> list = new List<DriverLicense>();
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
        public async Task<DriverLicense> Get(int id, int type)
        {
            DriverLicense list = new DriverLicense();
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
