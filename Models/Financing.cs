using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Financing
    {
        public int Id { get; set; }
        public Sale Sale { get; set; }
        public DateTime FinancingDate { get; set; }
        public Bank Bank { get; set; }
    }
}
