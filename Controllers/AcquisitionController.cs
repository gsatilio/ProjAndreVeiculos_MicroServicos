using Models;
using Services;

namespace Controllers
{
    public class AcquisitionController
    {
        private AcquisitionService _service = new();

        public AcquisitionController()
        {

        }
        public int Insert(Acquisition acquisition, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(acquisition, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
