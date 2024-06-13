using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PaymentDTO
    {
        public int? IdCreditCard { get; set; }
        public int? IdBoleto { get; set; }
        public int? IdPix { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
