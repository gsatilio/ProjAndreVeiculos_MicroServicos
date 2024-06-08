using Models;
using Repositories;

namespace Services
{
    public class EmployeeService
    {
        private EmployeeRepository _repository = new();

        public int Insert(Employee employee, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(employee, type);
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
