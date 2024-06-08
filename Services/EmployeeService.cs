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
        public async Task<List<Employee>> GetAll(int type)
        {
            List<Employee> list = new List<Employee>();
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
        public async Task<Employee> Get(string document, int type)
        {
            Employee list = new Employee();
            try
            {
                list = await _repository.Get(document, type);
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
