using Models.DTO;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TermsOfUse
    {
        [BsonId]
        public int Id { get; set; }
        public string Text { get; set; }
        public int Version { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Status { get; set; }

        public TermsOfUse()
        {
            
        }

        public TermsOfUse(TermsOfUseDTO dto)
        {
            this.Text = dto.Text;
            this.Version = dto.Version;
            this.RegistrationDate = dto.RegistrationDate;
            this.Status = dto.Status;
        }
    }
}
