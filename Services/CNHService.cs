using Models;
using Repositories;

namespace Services
{
    public class CNHService
    {
        private CNHRepository _repository = new();

        public async Task<List<CNH>> GetAll(int type)
        {
            List<CNH> list = new List<CNH>();
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
        public async Task<CNH> Get(int id, int type)
        {
            CNH list = new CNH();
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
