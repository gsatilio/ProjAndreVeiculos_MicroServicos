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
        [Key]
        public string CNPJ { get; set; }
        public string BankName { get; set; }
        public DateTime FoundationDate { get; set; }

    }
}
