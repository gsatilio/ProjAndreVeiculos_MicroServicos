using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Insurance
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public decimal Deductible { get; set; }
        public Car Car { get; set; }
        public Conductor MainConductor { get; set; }
    }
}
