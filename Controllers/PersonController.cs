using Models;
using Services;

namespace Controllers
{
    public class PersonController
    {
        private PersonService _service = new();

        public PersonController()
        {

        }
        public int Insert((Address, DateOnly, string, string, string, string) person, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(person, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
