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
    }
}
