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
        public async Task<List<Employee>> GetAll(int type)
        {
            List<Employee> list = new List<Employee>();
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
        public async Task<Employee> Get(string document, int type)
        {
            Employee list = new Employee();
            try
            {
                list = await _service.Get(document, type);
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
