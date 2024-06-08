using Models;
using Services;

namespace Controllers
{
    public class EmployeeController
    {
        private EmployeeService _service = new();

        public EmployeeController()
        {

        }
        public int Insert(Employee Employee, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(Employee, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
