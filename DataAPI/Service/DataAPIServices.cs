using Models.DTO;
using Models.MyAPI;
using Models;
using Newtonsoft.Json;
using Humanizer;
using System.Net;

namespace DataAPI.Service
{
    public class DataAPIServices
    {
        /// Endpoints do Projeto
        private readonly string _Address = "https://localhost:7050/api/Addresses/";
        private readonly string _Car = "https://localhost:7025/api/Cars/";
        private readonly string _Customer = "https://localhost:7165/api/Customers/";
        private readonly string _Dependent = "https://localhost:7068/";
        private readonly string _Employee = "https://localhost:7079/";
        private readonly string _Operation = "https://localhost:7143/api/Operations/";
        private readonly string _TermsofUse = "https://localhost:7290/";


        private readonly string _Acquisition = "https://localhost:7237/";
        private readonly string _CarOperation = "https://localhost:7194/";
        private readonly string _FinancialPending = "https://localhost:7061/";
        private readonly string _Financing = "https://localhost:7050/";

        private readonly string _Driver = "https://localhost:7025/";
        private readonly string _Insurance = "https://localhost:7060/";
        private readonly string _Payment = "https://localhost:7133/";
        private readonly string _Sale = "https://localhost:7222/api/Sales/";

        public async Task<Sale> GetSaleAPI(int id)
        {
            Sale sale = new();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_Sale}{id},2";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        sale = JsonConvert.DeserializeObject<Sale>(responseBody);
                    }
                    else
                    {
                        sale = null;
                        Console.WriteLine("Erro no consumo do WS API Address.");
                        Console.WriteLine(response.StatusCode);
                    }
                }
                return sale;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Customer> GetCustomerAPI(string document)
        {
            Customer customer = new();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_Customer}{document},2";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        customer = JsonConvert.DeserializeObject<Customer>(responseBody);
                        customer.Address = await GetAddressAPI(customer.Address.Id);
                    }
                    else
                    {
                        customer = null;
                        Console.WriteLine("Erro no consumo do WS API Address.");
                        Console.WriteLine(response.StatusCode);
                    }
                }
                return customer;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Car> GetCarAPI(string document)
        {
            Car car = new();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_Car}{document},2";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        car = JsonConvert.DeserializeObject<Car>(responseBody);
                    }
                    else
                    {
                        car = null;
                        Console.WriteLine("Erro no consumo do WS API Address.");
                        Console.WriteLine(response.StatusCode);
                    }
                }
                return car;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Operation> GetOperationAPI(int id)
        {
            Operation car = new();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_Operation}{id},2";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        car = JsonConvert.DeserializeObject<Operation>(responseBody);
                    }
                    else
                    {
                        car = null;
                        Console.WriteLine("Erro no consumo do WS API Address.");
                        Console.WriteLine(response.StatusCode);
                    }
                }
                return car;
            }
            catch
            {
                throw;
            }
        }


        public async Task<Address> PostAddressAPI(AddressDTO dto)
        {
            Address address;
            AddressAPI addressAPI = new();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_Address}2";
                    string jsonAddress = JsonConvert.SerializeObject(dto);

                    var content = new StringContent(jsonAddress, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        address = new(JsonConvert.DeserializeObject<AddressAPI>(responseBody));
                    }
                    else
                    {
                        address = null;
                        Console.WriteLine("Erro no consumo do WS API Address.");
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

        public async Task<List<Address>> GetAllAddressAPI()
        {
            Address address = new();
            List<Address> addresses = new List<Address>();
            List<AddressAPI> addressesAPI = new List<AddressAPI>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_Address}2";
                    string jsonAddress = JsonConvert.SerializeObject(address);

                    var content = new StringContent(jsonAddress, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        addressesAPI = JsonConvert.DeserializeObject<List<AddressAPI>>(responseBody);
                        foreach (var item in addressesAPI)
                        {
                            addresses.Add(new Address(item));
                        }
                    }
                    else
                    {
                        addresses = null;
                        Console.WriteLine("Erro no consumo do WS API Address.");
                        Console.WriteLine(response.StatusCode);
                    }
                }
                return addresses;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Address> GetAddressAPI(int id)
        {
            Address address = new();
            AddressAPI addressAPI = new();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_Address}{id},2";
                    string jsonAddress = JsonConvert.SerializeObject(address);

                    var content = new StringContent(jsonAddress, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        address = new(JsonConvert.DeserializeObject<AddressAPI>(responseBody));
                    }
                    else
                    {
                        address = null;
                        Console.WriteLine("Erro no consumo do WS API Address.");
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
