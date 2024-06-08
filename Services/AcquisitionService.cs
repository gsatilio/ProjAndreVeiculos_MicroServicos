using Models;
using Repositories;

namespace Services
{
    public class AcquisitionService
    {
        private AcquisitionRepository _repository = new();

        public int Insert(Acquisition acquisition, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(acquisition, type);
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
