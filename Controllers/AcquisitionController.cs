using Models;
using Services;

namespace Controllers
{
    public class AcquisitionController
    {
        private AcquisitionService _service = new();

        public AcquisitionController()
        {

        }
        public int Insert(Acquisition acquisition, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(acquisition, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
        public async Task<List<Acquisition>> GetAll(int type)
        {
            List<Acquisition> list = new List<Acquisition>();
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
        public async Task<Acquisition> Get(int id, int type)
        {
            Acquisition list = new Acquisition();
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
