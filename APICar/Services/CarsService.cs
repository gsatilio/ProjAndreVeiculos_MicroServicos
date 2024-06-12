using Models;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using Repositories;

namespace APICar.Services
{
    public class CarsService
    {
        private CarRepository _repository = new();

        public void InsertFileOnSQL(string jsonString, int type)
        {
            var json = JsonConvert.DeserializeObject<CarList>(jsonString);
            _repository.Delete(type);

            _repository.InsertBatch(json, type);
        }
        public CarList Retrieve(int type)
        {
            CarList carList = new CarList();
            try
            {
                carList = _repository.Retrieve(type);
            }
            catch
            {
                throw;
            }
            return carList;
        }

        public async Task<string> Insert(Car car, int type)
        {
            string result = null;
            try
            {
                result = await _repository.Insert(car, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }

        public async Task<List<Car>> GetAll(int type)
        {
            List<Car> carList = new List<Car>();
            try
            {
                carList = await _repository.GetAll(type);
            }
            catch
            {
                throw;
            }
            return carList;
        }
        public async Task<Car> Get(string licensePlate, int type)
        {
            Car carList = new Car();
            try
            {
                carList = await _repository.Get(licensePlate, type);
            }
            catch
            {
                throw;
            }
            return carList;
        }
    }
}
