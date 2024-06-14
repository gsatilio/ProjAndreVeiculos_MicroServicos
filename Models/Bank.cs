using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bank
    {
        public static readonly string INSERT = " INSERT INTO Bank (CNPJ, BankName, FoundationDate) VALUES (@CNPJ, @BankName, @FoundationDate)";
        public static readonly string GETALL = " SELECT CNPJ, BankName, FoundationDate FROM Bank";
        public static readonly string GET = " SELECT CNPJ, BankName, FoundationDate FROM Bank WHERE CNPJ = @CNPJ";
        [Key]
        [BsonId]
        [JsonProperty("cnpj")]
        public string CNPJ { get; set; }
        [JsonProperty("bankname")]
        public string BankName { get; set; }
        [JsonProperty("foundationdate")]
        public DateTime FoundationDate { get; set; }

    }
}
