using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bank
    {
        public static readonly string INSERT = " INSERT INTO Bank (CNPJ, BankName, FoundationDate) VALUES (@CNPJ, @BankName, @FoundationDate)";
        public static readonly string GETALL = " SELECT CNPJ, BankName, FoundationDate FROM Bank";
        public static readonly string GET = " SELECT CNPJ, BankName, FoundationDate FROM Bank WHERE CNPJ = @CNPJ";
        [Key]
        public string CNPJ { get; set; }
        public string BankName { get; set; }
        public DateTime FoundationDate { get; set; }

    }
}
