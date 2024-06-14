using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class TermsOfUseDTO
    {
        public string Text { get; set; }
        public int Version { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Status { get; set; }
    }
}
