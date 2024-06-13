using Models;
using Repositories;

namespace APIFinancing.Services
{
    public class FinancingsService
    {
        private FinancingRepository _repository = new();

        public async Task<int> Insert(Financing employee, int type)
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
        public async Task<List<Financing>> GetAll(int type)
        {
            List<Financing> list = new List<Financing>();
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
        public async Task<Financing> Get(int id, int type)
        {
            Financing list = new Financing();
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
