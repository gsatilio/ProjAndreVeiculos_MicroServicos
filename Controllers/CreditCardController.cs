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
        public async Task<List<CreditCard>> GetAll(int type)
        {
            List<CreditCard> list = new List<CreditCard>();
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
        public async Task<CreditCard> Get(int id, int type)
        {
            CreditCard list = new CreditCard();
            try
            {
                list = await _service.Get(id, type);
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
