using Models;
using Services;

namespace Controllers
{
    public class BoletoController
    {
        private BoletoService _service = new();

        public BoletoController()
        {

        }
        public int Insert(Boleto boleto, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(boleto, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
