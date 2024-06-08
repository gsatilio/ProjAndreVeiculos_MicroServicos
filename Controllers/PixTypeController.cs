using Models;
using Services;

namespace Controllers
{
    public class PixTypeController
    {
        private PixTypeService _service = new();

        public PixTypeController()
        {

        }
        public int Insert(PixType pixType, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(pixType, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
