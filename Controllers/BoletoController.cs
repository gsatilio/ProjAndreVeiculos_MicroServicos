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

        public async Task<List<Boleto>> GetAll(int type)
        {
            List<Boleto> list = new List<Boleto>();
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
        public async Task<Boleto> Get(int id, int type)
        {
            Boleto list = new Boleto();
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
