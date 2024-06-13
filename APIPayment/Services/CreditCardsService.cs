using Models;
using Repositories;

namespace APIPayment.Services
{
    public class CreditCardsService
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

        public async Task<List<CreditCard>> GetAll(int type)
        {
            List<CreditCard> list = new List<CreditCard>();
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
        public async Task<CreditCard> Get(int id, int type)
        {
            CreditCard list = new CreditCard();
            try
            {
                list = await _repository.Get(id, type);
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
