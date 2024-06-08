using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;

namespace Controllers
{
    public class CarController
    {
        private CarService _carService = new CarService();
        public bool SaveCarDataFromAPI(string path, int type)
        {
            var response = false;
            try
            {
                var reader = new StreamReader(path);
                var json = reader.ReadToEnd();
                JToken.Parse(json);
                _carService.InsertFileOnSQL(json, type);
                response = true;
            }
            catch
            {
                response = false;
                throw;
            }
            return response;
        }
        public CarList Retrieve(int type)
        {
            CarList carList = new CarList();
            try
            {
                carList = _carService.Retrieve(type);
            }
            catch
            {
                throw;
            }
            return carList;
        }
    }
}
