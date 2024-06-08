using Models;
using Repositories;

namespace Services
{
    public class PixTypeService
    {
        private PixTypeRepository _repository = new();

        public int Insert(PixType pixType, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(pixType, type);
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
