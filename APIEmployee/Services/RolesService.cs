using Models;
using Repositories;

namespace APIEmployee.Services
{
    public class RolesService
    {
        private RoleRepository _repository = new();

        public async Task<int> Insert(Role role, int type)
        {
            int result = 0;
            try
            {
                result = await _repository.Insert(role, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }

        public async Task<List<Role>> GetAll(int type)
        {
            List<Role> list = new List<Role>();
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
        public async Task<Role> Get(int id, int type)
        {
            Role list = new Role();
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
