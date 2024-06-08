using Models;
using Newtonsoft.Json;
using Repositories;

namespace Services
{
    public class CarService
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
    }
}
