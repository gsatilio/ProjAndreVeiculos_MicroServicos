using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class InsuranceDTO
    {
        public int Id { get; set; }
        public string CustomerDocument { get; set; }
        public decimal Deductible { get; set; }
        public string CarLicense { get; set; }
        public string MainConductorDocument { get; set; }
    }
}
