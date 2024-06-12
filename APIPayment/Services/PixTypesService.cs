using Models;
using Repositories;

namespace APIPayment.Services
{
    public class PixTypesService
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

        public async Task<List<PixType>> GetAll(int type)
        {
            List<PixType> list = new List<PixType>();
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
        public async Task<PixType> Get(int id, int type)
        {
            PixType list = new PixType();
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
