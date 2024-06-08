using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class CarOperationController
    {
        private CarOperationService _service = new();
        public CarOperationController()
        {
            
        }
        public int Insert(CarOperation carOp, int type)
        {
            int idService = 0;
            try
            {
                idService = _service.Insert(carOp, type);
            }
            catch
            {
                throw;
            }
            return idService;
        }

        public int ChangeStatusCarServiceTable(CarOperation carOp, int type)
        {
            int idService = 0;
            try
            {
                idService = _service.ChangeStatusCarServiceTable(carOp, type);
            }
            catch
            {
                throw;
            }
            return idService;
        }

        public CarOperationList Retrieve(int type)
        {
            CarOperationList carOpList = new CarOperationList();
            try
            {
                carOpList = _service.Retrieve(type);
            }
            catch
            {
                throw;
            }
            return carOpList;
        }

        public CarOperationList RetrieveCarServiceTableStatus(bool status, int type)
        {
            CarOperationList carOpList = new CarOperationList();
            try
            {
                carOpList = _service.RetrieveCarServiceTableStatus(status, type);
            }
            catch
            {
                throw;
            }
            return carOpList;
        }
    }
}
