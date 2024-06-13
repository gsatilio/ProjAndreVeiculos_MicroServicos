using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class FinancialPendingDTO
    {
        public string Document { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime PendingDate { get; set; }
        public DateTime BillingDate { get; set; }
        public bool Status { get; set; }
    }
}
