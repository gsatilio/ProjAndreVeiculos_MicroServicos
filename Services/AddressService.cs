using Models;
using Repositories;
using System.Collections.Generic;
using System.Net;


namespace Services
{
    public class AddressService
    {
        private AddressRepository _repository = new();

        public int Insert(Address address, int type)
        {
            int result = 0;
            try
            {
                result = _repository.Insert(address, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
        public async Task<List<Address>> GetAll(int type)
        {
            List<Address> list = new List<Address>();
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
        public async Task<Address> Get(int id, int type)
        {
            Address list = new Address();
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
