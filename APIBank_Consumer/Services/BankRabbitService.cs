using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIBank.Services
{
    public class BankRabbitService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<Bank> PostBankToMongo(Bank bank)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(bank), Encoding.UTF8, "application/json");
                HttpResponseMessage respose = await BankRabbitService._httpClient.PostAsync("https://localhost:7234/api/Banks", content);
                // porta 7234 é a do SendMessage
                respose.EnsureSuccessStatusCode();
                string bankReturn = await respose.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Bank>(bankReturn);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Bank> PostBankToSQL(Bank bank)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(bank), Encoding.UTF8, "application/json");
                HttpResponseMessage respose = await BankRabbitService._httpClient.PostAsync("https://localhost:7128/api/Banks", content);
                // porta 7234 é a do SendMessage
                respose.EnsureSuccessStatusCode();
                string bankReturn = await respose.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Bank>(bankReturn);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
