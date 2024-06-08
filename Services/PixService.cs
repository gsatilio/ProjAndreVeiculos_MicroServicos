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
    }
}
