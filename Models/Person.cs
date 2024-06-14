using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Person
    {
        [Key]
        [JsonProperty("document")]
        public string Document { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("dateofbirth")]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("address")]
        public Address Address { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }

    }
}
