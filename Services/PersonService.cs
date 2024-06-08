using Models;
using Repositories;

namespace Services
{
    public class PersonService
    {
        private PersonRepository _repository = new();

        public int Insert((Address, DateOnly, string, string, string, string) person, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(person, type);
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
