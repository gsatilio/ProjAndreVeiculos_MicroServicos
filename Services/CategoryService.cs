using Models;
using Repositories;

namespace Services
{
    public class CategoryService
    {
        private CategoryRepository _repository = new();

        public async Task<List<Category>> GetAll(int type)
        {
            List<Category> list = new List<Category>();
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
        public async Task<Category> Get(int id, int type)
        {
            Category list = new Category();
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
