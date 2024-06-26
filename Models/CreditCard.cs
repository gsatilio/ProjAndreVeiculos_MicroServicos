﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CreditCard
    {
        public readonly static string INSERT = " INSERT INTO CreditCard (CardNumber, SecurityCode, ExpirationDate, CardName) VALUES (@CardNumber, @SecurityCode, @ExpirationDate, @CardName); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT Id, CardNumber, SecurityCode, ExpirationDate, CardName FROM CreditCard ";
        public readonly static string GET = " SELECT Id, CardNumber, SecurityCode, ExpirationDate, CardName FROM CreditCard WHERE Id = @IdCreditCard ";
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("cardnumber")]
        public string CardNumber { get; set; }
        [JsonProperty("securitycode")]
        public string SecurityCode { get; set; }
        [JsonProperty("expirationdate")]
        public string ExpirationDate { get; set; }
        [JsonProperty("cardname")]
        public string CardName { get; set; }
    }
}
