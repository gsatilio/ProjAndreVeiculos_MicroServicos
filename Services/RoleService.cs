using Models;
using Repositories;

namespace Services
{
    public class RoleService
    {
        private RoleRepository _repository = new();

        public int Insert(Role role, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(role, type);
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
