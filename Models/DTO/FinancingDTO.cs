using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class FinancingDTO
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public DateTime FinancingDate { get; set; }
        public string BankCNPJ { get; set; }
    }
}
