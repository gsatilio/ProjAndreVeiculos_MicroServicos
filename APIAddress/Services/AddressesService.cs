using Models;
using Models.DTO;
using MongoDB.Driver;
using Newtonsoft.Json;
using MongoDB;
using NuGet.Protocol.Core.Types;
using Repositories;

namespace APIAddress.Services
{
    public class AddressesService
    {
        private readonly IMongoCollection<Address> _address;
        private AddressRepository _repository;
        private readonly string _url = "https://viacep.com.br/ws";

        public AddressesService(IMongoDBAPIDataBaseSettings settings, AddressRepository addressRepository)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);
            _repository = addressRepository;
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

        public async Task<int> Insert(Address address, int type)
        {
            int result = 0;
            try
            {
                result = await _repository.Insert(address, type);
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
