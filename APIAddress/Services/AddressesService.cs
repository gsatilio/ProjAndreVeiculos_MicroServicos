using APIAddress.Utils;
using Models;
using Models.DTO;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;

namespace APIAddress.Services
{
    public class AddressesService
    {
        private readonly string _url = "https://viacep.com.br/ws";

        private readonly IMongoCollection<Address> _address;

        public AddressesService(IMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);
        }
        public void InsertMongo(Address address)
        {
            if (address != null)
                _address.InsertOne(address);
        }
        public async Task<Address> RetrieveAdressAPI(AddressDTO addressDTO)
        {
            Address address = new();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_url}/{addressDTO.CEP}/json/";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        address = JsonConvert.DeserializeObject<Address>(json);
                        address.CEP = addressDTO.CEP;
                        address.Complement = addressDTO.Complement;
                        address.Number = addressDTO.Number;
                        address.StreetType = addressDTO.StreetType;
                    }
                    else
                    {
                        address = null;
                        Console.WriteLine("Erro no consumo do WS CEP.");
                        Console.WriteLine(response.StatusCode);
                    }
                }
                return address;
            }
            catch
            {
                throw;
            }
        }
    }
}
