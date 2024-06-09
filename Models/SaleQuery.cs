using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SaleQuery
    {
        public Sale Sale { get; set; }
        public Car Car { get; set; }
        public Customer Customer { get; set; }

        public Address CustomerAddress { get; set; }
        public Employee Employee { get; set; }
        public Address EmployeeAddress { get; set; }
        public Role Role { get; set; }
        public Payment Payment { get; set; }
        public CreditCard CreditCard { get; set; }
        public Boleto Boleto { get; set; }
        public Pix Pix { get; set; }
        public PixType PixType { get; set; }
    }
}
