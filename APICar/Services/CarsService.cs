using Models;
using Newtonsoft.Json;
using Repositories;

namespace APICar.Services
{
    public class CarsService
    {
        private CarRepository _carRepository = new();

        public void InsertFileOnSQL(string jsonString, int type)
        {
            var json = JsonConvert.DeserializeObject<CarList>(jsonString);
            _carRepository.Delete(type);

            _carRepository.InsertBatch(json, type);
        }
        public CarList Retrieve(int type)
        {
            CarList carList = new CarList();
            try
            {
                carList = _carRepository.Retrieve(type);
            }
            catch
            {
                throw;
            }
            return carList;
        }

        public async Task<List<Car>> GetAll(int type)
        {
            List<Car> carList = new List<Car>();
            try
            {
                carList = await _carRepository.GetAll(type);
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
                carList = await _carRepository.Get(licensePlate, type);
            }
            catch
            {
                throw;
            }
            return carList;
        }
    }
}
