using Models;
using Services;

namespace Controllers
{
    public class CreditCardController
    {
        private CreditCardService _service = new();

        public CreditCardController()
        {

        }
        public int Insert(CreditCard creditCard, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(creditCard, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
