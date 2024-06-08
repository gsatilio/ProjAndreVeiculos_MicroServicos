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
    }
}
