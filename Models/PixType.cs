using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PixType
    {
        public readonly static string INSERT = " INSERT INTO PIXTYPE (Name) VALUES (@Name); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT Id, Name FROM PixType ";
        public readonly static string GET = " SELECT Id, Name FROM PixType WHERE Id = @IdPixType ";
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
