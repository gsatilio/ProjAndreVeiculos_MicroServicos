using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Insurance
    {
        public readonly static string INSERT = " INSERT INTO Insurance (CustomerDocument, Deductible, CarLicensePlate, MainConductorDocument) VALUES (@CustomerDocument, @Deductible, @CarLicensePlate, @MainConductorDocument); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT Id, CustomerDocument, Deductible, CarLicensePlate, MainConductorDocument FROM Insurance ";
        public readonly static string GET = " SELECT Id, CustomerDocument, Deductible, CarLicensePlate, MainConductorDocument FROM Insurance WHERE Id = @IdInsurance ";

        public int Id { get; set; }
        public Customer Customer { get; set; }
        public decimal Deductible { get; set; }
        public Car Car { get; set; }
        public Conductor MainConductor { get; set; }
    }
}
