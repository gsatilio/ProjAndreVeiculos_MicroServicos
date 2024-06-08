using Models;
using Services;

namespace Controllers
{
    public class PaymentController
    {
        private PaymentService _service = new();

        public PaymentController()
        {

        }
        public int Insert(Payment Payment, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(Payment, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
