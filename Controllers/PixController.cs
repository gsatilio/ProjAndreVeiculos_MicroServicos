using Models;
using Services;

namespace Controllers
{
    public class PixController
    {
        private PixService _service = new();

        public PixController()
        {

        }
        public int Insert(Pix pix, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(pix, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
        public async Task<List<Pix>> GetAll(int type)
        {
            List<Pix> list = new List<Pix>();
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
        public async Task<Pix> Get(int id, int type)
        {
            Pix list = new Pix();
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
