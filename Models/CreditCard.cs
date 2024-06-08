using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CreditCard
    {
        public readonly static string INSERT = " INSERT INTO CreditCard (CardNumber, SecurityCode, ExpirationDate, CardName) VALUES (@CardNumber, @SecurityCode, @ExpirationDate, @CardName); SELECT cast(scope_identity() as int) ";
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string SecurityCode { get; set; }
        public string ExpirationDate { get; set; }
        public string CardName { get; set; }
    }
}
