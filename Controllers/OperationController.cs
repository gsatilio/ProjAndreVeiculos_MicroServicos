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
    }
}
