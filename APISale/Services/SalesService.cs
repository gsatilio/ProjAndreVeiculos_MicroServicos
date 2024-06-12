using Models;
using Repositories;

namespace APISale.Services
{
    public class SalesService
    {
        private SaleRepository _repository = new();

        public int Insert(Sale sale, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(sale, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }

        public async Task<List<Sale>> GetAll(int type)
        {
            List<Sale> carList = new List<Sale>();
            try
            {
                carList = await _repository.GetAll(type);
            }
            catch
            {
                throw;
            }
            return carList;
        }
        public async Task<Sale> Get(int id, int type)
        {
            Sale carList = new Sale();
            try
            {
                carList = await _repository.Get(id, type);
            }
            catch
            {
                throw;
            }
            return carList;
        }
    }
}
