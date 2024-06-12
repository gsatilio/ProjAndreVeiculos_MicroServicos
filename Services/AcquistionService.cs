using Models;
using Repositories;

namespace Services
{
    public class AcquisitionService
    {
        private AcquisitionRepository _repository = new();

        public async Task<int> Insert(Acquisition acquisition, int type)
        {
            int result = 0;
            try
            {
                result = await _repository.Insert(acquisition, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
        public async Task<List<Acquisition>> GetAll(int type)
        {
            List<Acquisition> list = new List<Acquisition>();
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
        public async Task<Acquisition> Get(int id, int type)
        {
            Acquisition list = new Acquisition();
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