using Models;
using Repositories;

namespace Services
{
    public class DriverLicenseService
    {
        private DriverLicenseRepository _repository = new();
        public async Task<long> Insert(DriverLicense cnh, int type)
        {
            long result = 0;
            try
            {
                result = await _repository.Insert(cnh, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
        public async Task<List<DriverLicense>> GetAll(int type)
        {
            List<DriverLicense> list = new List<DriverLicense>();
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
        public async Task<DriverLicense> Get(int id, int type)
        {
            DriverLicense list = new DriverLicense();
            try
            {
                list = await _repository.Get(id, type);
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
