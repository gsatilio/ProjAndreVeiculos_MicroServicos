using Models;
using Repositories;

namespace APIPayment.Services
{
    public class BoletosService
    {
        private BoletoRepository _repository = new();

        public async Task<int> Insert(Boleto acquisition, int type)
        {
            int result = 0;
            try
            {
                result = await _repository.Insert(acquisition, type);
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
