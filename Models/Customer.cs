using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Customer : Person
    {
        //public readonly static string INSERT = " INSERT INTO CUSTOMER (Document, Income, PDFDocument) VALUES (@Document, @Income, @PDFDocument); SELECT cast(scope_identity() as int) ";
        public readonly static string INSERT = " INSERT INTO CUSTOMER (Document, Income, PDFDocument, Name, DateOfBirth, AddressId, Phone, Email) VALUES (@Document, @Income, @PDFDocument, @Name, @DateOfBirth, @AddressId, @Phone, @Email); SELECT cast(scope_identity() as int) ";
        /*public readonly static string INSERTPERSON = " IF NOT EXISTS (SELECT Document FROM PERSON WHERE Document = @Document) BEGIN " +
            " INSERT INTO PERSON (Document, Name, DateOfBirth, IdAddress, Phone, Email)" +
            " VALUES (@Document, @Name, @DateOfBirth, @IdAddress, @Phone, @Email) END;";*/
        public Decimal Income { get; set; }
        public string PDFDocument { get; set; }
    }
}
