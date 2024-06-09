using Models;
using Services;

namespace Controllers
{
    public class PixTypeController
    {
        private PixTypeService _service = new();

        public PixTypeController()
        {

        }
        public int Insert(PixType pixType, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(pixType, type);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<List<PixType>> GetAll(int type)
        {
            List<PixType> list = new List<PixType>();
            try
            {
                list = await _service.GetAll(type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return list;
        }
        public async Task<PixType> Get(int id, int type)
        {
            PixType list = new PixType();
            try
            {
                list = await _service.Get(id, type);
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
