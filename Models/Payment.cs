using Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Payment
    {
        public readonly static string INSERT = " INSERT INTO PAYMENT (IdCreditCard, IdBoleto, IdPix, PaymentDate) VALUES (@IdCreditCard, @IdBoleto, @IdPix, @PaymentDate); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT A.Id, A.PaymentDate, B.Id, B.CardName, B.CardNumber, B.ExpirationDate, B.SecurityCode, C.Id, C.ExpirationDate, C.Number, D.Id, D.PixKey, E.Id, E.Name FROM Payment A INNER JOIN CreditCard B ON A.CreditCardId = B.Id INNER JOIN Boleto C ON A.BoletoId = C.Id INNER JOIN Pix D ON A.PixId = D.Id INNER JOIN PixType E ON D.PixTypeId = E.Id ";
        public readonly static string GET = " SELECT A.Id, A.PaymentDate, B.Id, B.CardName, B.CardNumber, B.ExpirationDate, B.SecurityCode, C.Id, C.ExpirationDate, C.Number, D.Id, D.PixKey, E.Id, E.Name FROM Payment A INNER JOIN CreditCard B ON A.CreditCardId = B.Id INNER JOIN Boleto C ON A.BoletoId = C.Id INNER JOIN Pix D ON A.PixId = D.Id INNER JOIN PixType E ON D.PixTypeId = E.Id WHERE A.Id = @IdPayment ";
        public int Id { get; set; }
        public CreditCard? CreditCard { get; set; }
        public Boleto? Boleto { get; set; }
        public Pix? Pix { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payment()
        {
            
        }

        public Payment(PaymentDTO paymentDTO)
        {
            this.PaymentDate = paymentDTO.PaymentDate;
        }
    }
}
