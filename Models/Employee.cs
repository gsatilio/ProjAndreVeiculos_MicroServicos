using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee : Person
    {
        //public readonly static string INSERT = " INSERT INTO EMPLOYEE (Document, IdRole, ComissionValue, Comission) VALUES (@Document, @IdRole, @ComissionValue, @Comission); SELECT cast(scope_identity() as int) ";
        public readonly static string INSERT = " INSERT INTO EMPLOYEE (Document, RoleId, ComissionValue, Comission, Name, DateOfBirth, AddressId, Phone, Email) VALUES (@Document, @RoleId, @ComissionValue, @Comission, @Name, @DateOfBirth, @AddressId, @Phone, @Email); SELECT cast(scope_identity() as int) ";
        /*public readonly static string INSERTPERSON = " IF NOT EXISTS (SELECT Document FROM PERSON WHERE Document = @Document) BEGIN " +
            " INSERT INTO PERSON (Document, Name, DateOfBirth, IdAddress, Phone, Email)" +
            " VALUES (@Document, @Name, @DateOfBirth, @IdAddress, @Phone, @Email) END;";*/
        public Role Role { get; set; }
        public Decimal ComissionValue { get; set; }
        public Decimal Comission { get; set; }
    }
}
