﻿using Models;
using Models.DTO;
using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;

namespace APIAddress.Services
{
    public class AddressesService
    {
        private readonly string _url = "https://viacep.com.br/ws";
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
                        address.StreetType = addressDTO.StreetType;
                        address.Complement = addressDTO.Complement;
                        address.Number = addressDTO.Number;
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