using Models;
using Services;

namespace Controllers
{
    public class ConductorController
    {
        private ConductorService _service = new();

        public ConductorController()
        {

        }
        public int Insert(Conductor conductor, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(conductor, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
        public async Task<List<Conductor>> GetAll(int type)
        {
            List<Conductor> list = new List<Conductor>();
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
        public async Task<Conductor> Get(int id, int type)
        {
            Conductor list = new Conductor();
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
