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
    }
}
