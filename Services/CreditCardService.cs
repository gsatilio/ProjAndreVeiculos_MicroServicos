using Models;
using Repositories;

namespace Services
{
    public class CreditCardService
    {
        private CreditCardRepository _repository = new();

        public int Insert(CreditCard creditCard, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(creditCard, type);
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
