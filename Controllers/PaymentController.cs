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

        public async Task<List<Payment>> GetAll(int type)
        {
            List<Payment> list = new List<Payment>();
            try
            {
                list = await _service.GetAll(type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return list;
        }
        public async Task<Payment> Get(int id, int type)
        {
            Payment list = new Payment();
            try
            {
                list = await _service.Get(id, type);
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
