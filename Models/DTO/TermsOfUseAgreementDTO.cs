using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class TermsOfUseAgreementDTO
    {
        public string CustomerDocument { get; set; }
        public int IdTermOfUse { get; set; }
        public DateTime AcceptanceDate { get; set; }
    }
}
