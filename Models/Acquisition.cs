using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Acquisition
    {
        public readonly static string INSERT = " INSERT INTO Acquisition (LicensePlate, Price, AcquisitionDate) VALUES (@LicensePlate, @Price, @AcquisitionDate); SELECT cast(scope_identity() as int) ";

        public int Id { get; set; }
        public Car Car { get; set; }
        public Decimal Price { get; set; }
        public DateTime AcquisitionDate { get; set; }

    }
}