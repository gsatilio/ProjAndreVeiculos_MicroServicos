using Models;
using Repositories;

namespace APIFinancialPending.Services
{
    public class FinancialPendingsService
    {
        private FinancialPendingRepository _repository = new();

        public async Task<int> Insert(FinancialPending employee, int type)
        {
            int result = 0;
            try
            {
                result = await _repository.Insert(employee, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
        public async Task<List<FinancialPending>> GetAll(int type)
        {
            List<FinancialPending> list = new List<FinancialPending>();
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
        public async Task<FinancialPending> Get(int id, int type)
        {
            FinancialPending list = new FinancialPending();
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
