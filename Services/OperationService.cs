﻿using Models;
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

        public async Task<List<Operation>> GetAll(int type)
        {
            List<Operation> list = new List<Operation>();
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
        public async Task<Operation> Get(int id, int type)
        {
            Operation list = new Operation();
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
