using Models;
using Repositories;

namespace APIDriver.Services
{
    public class ConductorsService
    {
        private ConductorRepository _repository = new();
        public async Task<string> Insert(Conductor conductor, int type)
        {
            string result = null;
            try
            {
                result = await _repository.Insert(conductor, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }

        public async Task<List<Conductor>> GetAll(int type)
        {
            List<Conductor> conductorList = new List<Conductor>();
            try
            {
                conductorList = await _repository.GetAll(type);
            }
            catch
            {
                throw;
            }
            return conductorList;
        }
        public async Task<Conductor> Get(string document, int type)
        {
            Conductor conductorList = new Conductor();
            try
            {
                conductorList = await _repository.Get(document, type);
            }
            catch
            {
                throw;
            }
            return conductorList;
        }
    }
}
