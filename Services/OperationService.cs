using Models;
using Repositories;

namespace Services
{
    public class OperationService
    {
        private OperationRepository _repository = new();

        public int Insert(Operation operation, int type)
        {
            int idService = 0;
            try
            {
                idService =  _repository.Insert(operation, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return idService;
        }

        public OperationList Retrieve()
        {
            OperationList opList = new OperationList();
            try
            {
                opList = _repository.Retrieve();
            }
            catch
            {
                throw;
            }
            return opList;
        }
    }
}
