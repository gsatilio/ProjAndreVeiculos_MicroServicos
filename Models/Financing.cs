using Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Financing
    {
        public readonly static string INSERT = " INSERT INTO Financing (SaleId, FinancingDate, BankCNPJ) VALUES (@SaleId, @FinancingDate, @BankCNPJ) ";
        public readonly static string GETALL = " SELECT Id, SaleId, FinancingDate, BankCNPJ FROM Financing ";
        public readonly static string GET = " SELECT Id, SaleId, FinancingDate, BankCNPJ FROM Financing WHERE Id = @FinancingId ";
        [Key]
        public int Id { get; set; }
        public Sale Sale { get; set; }
        public DateTime FinancingDate { get; set; }
        public Bank Bank { get; set; }

        public Financing()
        {
            
        }

        public Financing(FinancingDTO financingDTO)
        {
            this.Id = financingDTO.Id;
            this.FinancingDate = financingDTO.FinancingDate;
        }
    }
}
