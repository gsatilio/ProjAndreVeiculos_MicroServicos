using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Payment
    {
        public readonly static string INSERT = " INSERT INTO PAYMENT (IdCreditCard, IdBoleto, IdPix, PaymentDate) VALUES (@IdCreditCard, @IdBoleto, @IdPix, @PaymentDate); SELECT cast(scope_identity() as int) ";
        public int Id { get; set; }
        public CreditCard CreditCard { get; set; }
        public Boleto Boleto { get; set; }
        public Pix Pix { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
