using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class AcquisitionDTO
    {
        public string LicensePlate { get; set; }
        public Decimal Price { get; set; }
        public DateTime AcquisitionDate { get; set; }
    }
}
