using Models;
using Services;

namespace Controllers
{
    public class RoleController
    {
        private RoleService _service = new();

        public RoleController()
        {

        }
        public int Insert(Role role, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(role, type);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<List<Role>> GetAll(int type)
        {
            List<Role> list = new List<Role>();
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
        public async Task<Role> Get(int id, int type)
        {
            Role list = new Role();
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
