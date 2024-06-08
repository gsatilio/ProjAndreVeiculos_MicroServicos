using Models;
using Repositories;

namespace Services
{
    public class PaymentService
    {
        private PaymentRepository _repository = new();

        public int Insert(Payment payment, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(payment, type);
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
