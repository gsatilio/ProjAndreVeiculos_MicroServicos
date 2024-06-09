using Models;
using Services;

namespace Controllers
{
    public class OperationController
    {
        private OperationService _service = new();

        public OperationController()
        {

        }
        public int Insert(Operation operation, int type)
        {
            int idService = 0;
            try
            {
                idService = _service.Insert(operation, type);
            }
            catch
            {
                throw;
            }
            return idService;
        }
        public OperationList Retrieve()
        {
            OperationList opList = new OperationList();
            try
            {
                opList = _service.Retrieve();
            }
            catch
            {
                throw;
            }
            return opList;
        }

        public async Task<List<Operation>> GetAll(int type)
        {
            List<Operation> list = new List<Operation>();
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
        public async Task<Operation> Get(int id, int type)
        {
            Operation list = new Operation();
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
