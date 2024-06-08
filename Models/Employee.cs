using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee : Person
    {
        public readonly static string INSERT = " INSERT INTO EMPLOYEE (Document, RoleId, ComissionValue, Comission, Name, DateOfBirth, AddressId, Phone, Email) VALUES (@Document, @RoleId, @ComissionValue, @Comission, @Name, @DateOfBirth, @AddressId, @Phone, @Email); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT A.DateOfBirth, A.Document, A.Email, A.Name, A.Comission, A.ComissionValue, A.Phone, B.CEP, B.City, B.Complement, B.Id, B.Neighborhood, B.Number, B.Street, B.StreetType, B.Uf, C.Id, C.Description FROM Employee A INNER JOIN Address B ON A.AddressId = B.Id INNER JOIN Role C ON A.RoleId = C.Id ";
        public Role Role { get; set; }
        public Decimal ComissionValue { get; set; }
        public Decimal Comission { get; set; }
    }
}
