using Models;
using Repositories;

namespace APIBank_SQL.Services
{
    public class BankSendMessageSQLService
    {
        private BankRepository _repository;

        public BankSendMessageSQLService()
        {
            _repository = new BankRepository();
        }

        public async Task<string> Insert(Bank acquisition, int type)
        {
            string result = null;
            try
            {
                result = await _repository.Insert(acquisition, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
        public async Task<List<Bank>> GetAll(int type)
        {
            List<Bank> list = new List<Bank>();
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
        public async Task<Bank> Get(string cnpj, int type)
        {
            Bank list = new Bank();
            try
            {
                list = await _repository.Get(cnpj, type);
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
