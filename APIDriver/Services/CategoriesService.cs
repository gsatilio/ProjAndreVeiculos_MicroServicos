using Models;
using NuGet.Protocol.Core.Types;
using Repositories;

namespace APIDriver.Services
{
    public class CategoriesService
    {
        private CategoryRepository _repository = new();
        public async Task<int> Insert(Category car, int type)
        {
            int result = 0;
            try
            {
                result = await _repository.Insert(car, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }

        public async Task<List<Category>> GetAll(int type)
        {
            List<Category> carList = new List<Category>();
            try
            {
                carList = await _repository.GetAll(type);
            }
            catch
            {
                throw;
            }
            return carList;
        }
        public async Task<Category> Get(int id, int type)
        {
            Category carList = new Category();
            try
            {
                carList = await _repository.Get(id, type);
            }
            catch
            {
                throw;
            }
            return carList;
        }
    }
}
