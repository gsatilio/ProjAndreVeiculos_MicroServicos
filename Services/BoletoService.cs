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

        public async Task<List<Boleto>> GetAll(int type)
        {
            List<Boleto> list = new List<Boleto>();
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
        public async Task<Boleto> Get(int id, int type)
        {
            Boleto list = new Boleto();
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
