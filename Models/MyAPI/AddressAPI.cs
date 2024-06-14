using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.MyAPI
{
    public class AddressAPI
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("cep")]
        public string CEP { get; set; }
        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }
        [JsonProperty("streetType")]
        public string StreetType { get; set; }
        [JsonProperty("complement")]
        public string Complement {  get; set; }
        [JsonProperty("number")]
        public int Number {  get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }

    }
}
