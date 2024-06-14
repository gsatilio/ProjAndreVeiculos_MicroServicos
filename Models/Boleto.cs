using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Boleto
    {
        public readonly static string INSERT = " INSERT INTO Boleto (Number, ExpirationDate) VALUES (@Number, @ExpirationDate); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT Id, Number, ExpirationDate FROM Boleto ";
        public readonly static string GET = " SELECT Id, Number, ExpirationDate FROM Boleto WHERE Id = @IdBoleto ";
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("number")]
        public int Number { get; set; }
        [JsonProperty("expirationdate")]
        public DateTime ExpirationDate { get; set; }
    }
}
