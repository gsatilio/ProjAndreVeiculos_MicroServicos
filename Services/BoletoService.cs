using Models;
using Repositories;

namespace Services
{
    public class BoletoService
    {
        private BoletoRepository _repository = new();

        public int Insert(Boleto boleto, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(boleto, type);
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
