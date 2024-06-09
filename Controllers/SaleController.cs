using Models;
using Services;

namespace Controllers
{
    public class SaleController
    {
        private SaleService _service = new();

        public SaleController()
        {

        }
        public int Insert(Sale sale, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(sale, type);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<List<Sale>> GetAll(int type)
        {
            List<Sale> list = new List<Sale>();
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
        public async Task<Sale> Get(int id, int type)
        {
            Sale list = new Sale();
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
