using Models;
using Repositories;

namespace APIPayment.Services
{
    public class PaymentsService
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
        public async Task<List<Payment>> GetAll(int type)
        {
            List<Payment> list = new List<Payment>();
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
        public async Task<Payment> Get(int id, int type)
        {
            Payment list = new Payment();
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
