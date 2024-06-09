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
        public readonly static string GETALL = " SELECT A.DateOfBirth, A.Document, A.Email, A.Income, A.Name, A.PDFDocument, A.Phone, B.CEP, B.City, B.Complement, B.Id, B.Neighborhood, B.Number, B.Street, B.StreetType, B.Uf FROM Customer A INNER JOIN Address B ON A.AddressId = B.Id ";
        public readonly static string GET = " SELECT A.DateOfBirth, A.Document, A.Email, A.Income, A.Name, A.PDFDocument, A.Phone, B.CEP, B.City, B.Complement, B.Id, B.Neighborhood, B.Number, B.Street, B.StreetType, B.Uf FROM Customer A INNER JOIN Address B ON A.AddressId = B.Id WHERE A.Document = @Document ";
        public Decimal Income { get; set; }
        public string PDFDocument { get; set; }
    }
}
