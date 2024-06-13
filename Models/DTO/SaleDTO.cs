using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class SaleDTO
    {
        public string LicensePlate { get; set; }
        public string CustomerDocument { get; set; }
        public string EmployeeDocument { get; set; }
        public int IdPayment { get; set; }
        public DateTime SaleDate { get; set; }
        public Decimal SaleValue { get; set; }
    }
}
