using Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Address
    {
        public readonly static string INSERT = " INSERT INTO Address (Street, CEP, Neighborhood, StreetType, Complement, Number, Uf, City) " +
            "VALUES (@Street, @CEP, @Neighborhood, @StreetType, @Complement, @Number, @Uf, @City); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT Street, CEP, Neighborhood, StreetType, Complement, Number, Uf, City FROM Address";
        public readonly static string GET = " SELECT Street, CEP, Neighborhood, StreetType, Complement, Number, Uf, City FROM Address WHERE Id = @Id";
        public int Id { get; set; }
        [JsonProperty("logradouro")]
        public string Street { get; set; }
        public string CEP { get; set; }
        [JsonProperty("bairro")]
        public string Neighborhood { get; set; }
        public string? StreetType { get; set; }
        public string? Complement { get; set; }
        public int Number { get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
        [JsonProperty("localidade")]
        public string City { get; set; }

        public Address()
        {
            
        }

        public Address (AddressDTO addressDTO)
        {
            this.StreetType = addressDTO.StreetType;
            this.CEP = addressDTO.CEP;
            this.Complement = addressDTO.Complement;
            this.Number = addressDTO.Number;
        }
    }
}
