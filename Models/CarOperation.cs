using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CarOperation
    {
        public readonly static string INSERT = " INSERT INTO CarOperation (CarLicensePlate, OperationId, Status) VALUES (@CarLicensePlate, @OperationId, @Status); SELECT cast(scope_identity() as int) ";
        public readonly static string UPDATE = " UPDATE CarOperation SET Status = @Status WHERE Id = @Id; SELECT cast(scope_identity() as int)  ";
        public int Id { get; set; }
        public Car Car { get; set; }
        public Operation Operation { get; set; }
        public bool Status { get; set; }
    }
}
