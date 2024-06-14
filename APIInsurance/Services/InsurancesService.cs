using Models;
using Repositories;

namespace APIInsurance.Services
{
    public class InsurancesService
    {
        private InsuranceRepository _repository = new();

        public int Insert(Insurance sale, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(sale, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }

        public async Task<List<Insurance>> GetAll(int type)
        {
            List<Insurance> carList = new List<Insurance>();
            try
            {
                carList = await _repository.GetAll(type);
            }
            catch
            {
                throw;
            }
            return carList;
        }
        public async Task<Insurance> Get(int id, int type)
        {
            Insurance carList = new Insurance();
            try
            {
                carList = await _repository.Get(id, type);
            }
            catch
            {
                throw;
            }
            return carList;
        }

    }
}
