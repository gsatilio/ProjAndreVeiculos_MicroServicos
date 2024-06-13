using Models;
using Repositories;

namespace APIDependent.Services
{
    public class DependentsService
    {
        private DependentRepository _repository = new();

        public async Task<int> Insert(Dependent customer, int type)
        {
            int result = 0;
            try
            {
                result = await _repository.Insert(customer, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
        public async Task<List<Dependent>> GetAll(int type)
        {
            List<Dependent> list = new List<Dependent>();
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
        public async Task<Dependent> Get(string document, int type)
        {
            Dependent list = new Dependent();
            try
            {
                list = await _repository.Get(document, type);
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
