using Models;
using Repositories;

namespace APIDriver.Services
{
    public class DriverLicensesService
    {
        private DriverLicenseRepository _repository = new();
        public async Task<long> Insert(DriverLicense driverLicense, int type)
        {
            long result = 0;
            try
            {
                result = await _repository.Insert(driverLicense, type);
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
            List<DriverLicense> driverLicenseList = new List<DriverLicense>();
            try
            {
                driverLicenseList = await _repository.GetAll(type);
            }
            catch
            {
                throw;
            }
            return driverLicenseList;
        }
        public async Task<DriverLicense> Get(long driverId, int type)
        {
            DriverLicense driverLicenseList = new DriverLicense();
            try
            {
                driverLicenseList = await _repository.Get(driverId, type);
            }
            catch
            {
                throw;
            }
            return driverLicenseList;
        }
    }
}
