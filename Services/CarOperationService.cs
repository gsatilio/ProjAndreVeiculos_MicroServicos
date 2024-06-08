using Microsoft.Data.SqlClient;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    
    public class CarOperationService
    {
        CarOperationRepository _repository = new();
        public int Insert(CarOperation carOp, int type)
        {
            int idService = 0;
            try
            {
                idService = _repository.Insert(carOp, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return idService;
        }
        public int ChangeStatusCarServiceTable(CarOperation carOp, int type)
        {
            int idService = 0;
            try
            {
                idService = _repository.ChangeStatusCarServiceTable(carOp, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return idService;
        }
        public CarOperationList Retrieve(int type)
        {
            CarOperationList csList = new CarOperationList();
            try
            {
                csList = _repository.Retrieve(type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return csList;
        }

        public CarOperationList RetrieveCarServiceTableStatus(bool status, int type)
        {
            CarOperationList csList = new CarOperationList();
            try
            {
                csList = _repository.RetrieveCarServiceTableStatus(status, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return csList;
        }

        public async Task<List<CarOperation>> GetAll(int type)
        {
            List<CarOperation> list = new List<CarOperation>();
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
        public async Task<CarOperation> Get(int id, int type)
        {
            CarOperation list = new CarOperation();
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
