using Models;
using Services;

namespace Controllers
{
    public class CategoryController
    {
        private CategoryService _service = new();

        public CategoryController()
        {

        }
        public async Task<List<Category>> GetAll(int type)
        {
            List<Category> list = new List<Category>();
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
        public async Task<Category> Get(int id, int type)
        {
            Category list = new Category();
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
