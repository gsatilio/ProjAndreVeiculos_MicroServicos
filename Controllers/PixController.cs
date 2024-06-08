using Models;
using Services;

namespace Controllers
{
    public class PixController
    {
        private PixService _service = new();

        public PixController()
        {

        }
        public int Insert(Pix pix, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(pix, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
