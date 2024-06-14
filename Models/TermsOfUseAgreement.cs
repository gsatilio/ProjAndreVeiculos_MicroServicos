using Models.DTO;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TermsOfUseAgreement
    {
        [BsonId]
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public TermsOfUse TermsOfUse { get; set; }
        public DateTime AcceptanceDate { get; set; }

        public TermsOfUseAgreement()
        {

        }

        public TermsOfUseAgreement(TermsOfUseAgreementDTO dto)
        {
            this.AcceptanceDate = dto.AcceptanceDate;
        }
    }
}
