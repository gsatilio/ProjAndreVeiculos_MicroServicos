using Models;
using Repositories;

namespace Services
{
    public class SaleService
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
    }
}
