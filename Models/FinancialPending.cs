using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FinancialPending
    {
        public readonly static string INSERT = " INSERT INTO FinancialPending (Description, Value, PendingDate, BillingDate, Status, CustomerDocument) VALUES (@Description, @Value, @PendingDate, @BillingDate, @Status, @CustomerDocument); SELECT cast(scope_identity() as int)  ";
        public readonly static string GETALL = " SELECT Id, Description, Value, PendingDate, BillingDate, Status, CustomerDocument FROM FinancialPending ";
        public readonly static string GET = " SELECT Id, Description, Value, PendingDate, BillingDate, Status, CustomerDocument FROM FinancialPending WHERE Id = @IdFinancialPending ";
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime PendingDate { get; set; }
        public DateTime BillingDate { get; set; }
        public bool Status { get; set; }
        public Customer Customer { get; set; }

        public FinancialPending()
        {

        }

        public FinancialPending(FinancialPendingDTO financialPendingDTO)
        {
            this.Description = financialPendingDTO.Description;
            this.Value = financialPendingDTO.Value;
            this.PendingDate = financialPendingDTO.PendingDate;
            this.BillingDate = financialPendingDTO.BillingDate;
            this.Status = financialPendingDTO.Status;
        }
    }
}
