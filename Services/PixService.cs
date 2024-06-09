using Models;
using Repositories;

namespace Services
{
    public class PixService
    {
        private PixRepository _repository = new();

        public int Insert(Pix pix, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(pix, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
        public async Task<List<Pix>> GetAll(int type)
        {
            List<Pix> list = new List<Pix>();
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
        public async Task<Pix> Get(int id, int type)
        {
            Pix list = new Pix();
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
