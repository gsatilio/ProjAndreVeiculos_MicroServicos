using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Sale
    {
        public readonly static string INSERT = " INSERT INTO SALE (LicensePlate, SaleDate, SaleValue, Customer, Employee, IdPayment) " +
            "VALUES (@LicensePlate, @SaleDate, @SaleValue, @Customer, @Employee, @IdPayment); SELECT cast(scope_identity() as int) ";

        public int Id { get; set; }
        public Car Car { get; set; }
        public DateTime SaleDate { get; set; }
        public Decimal SaleValue { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Payment Payment { get; set; }

    }
}
