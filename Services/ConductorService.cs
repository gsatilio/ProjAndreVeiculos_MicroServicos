using Models;
using Repositories;

namespace Services
{
    public class ConductorService
    {
        private ConductorRepository _repository = new();

        public int Insert(Conductor conductor, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(conductor, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
        public async Task<List<Conductor>> GetAll(int type)
        {
            List<Conductor> list = new List<Conductor>();
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
        public async Task<Conductor> Get(int id, int type)
        {
            Conductor list = new Conductor();
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
