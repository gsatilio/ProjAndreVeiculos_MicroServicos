using Models;
using Models.DTO;
using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;

namespace APIAddress.Services
{
    public class AddressesService
    {
        private readonly string _url = "https://viacep.com.br/ws";
        public async Task<Address> RetrieveAdressAPI(Address address)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_url}/{address.CEP}/json/";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        address = JsonConvert.DeserializeObject<Address>(json);
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
