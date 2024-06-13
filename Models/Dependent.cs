using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Dependent : Person
    {
        public readonly static string INSERT = " INSERT INTO Dependent (Document, CustomerDocument, Name, DateOfBirth, AddressId, Phone, Email) VALUES (@Document, @CustomerDocument, @Name, @DateOfBirth, @AddressId, @Phone, @Email); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT A.DateOfBirth, A.Document, A.Email, A.Name, A.Phone, B.Id, B.City, B.Complement, B.CEP, B.Neighborhood, B.Number, B.Street, B.StreetType, B.Uf, C.Document, C.AddressId, C.DateOfBirth, C.Email, C.Income, C.Name, C.PDFDocument, C.Phone, D.Id, D.City, D.Complement, D.CEP, D.Neighborhood, D.Number, D.Street, D.StreetType, D.Uf FROM Dependent A INNER JOIN Address B ON A.AddressId = B.Id INNER JOIN Customer C ON A.CustomerDocument = C.Document INNER JOIN Address D ON C.AddressId = D.Id ";
        public readonly static string GET = " SELECT A.DateOfBirth, A.Document, A.Email, A.Name, A.Phone, B.Id, B.City, B.Complement, B.CEP, B.Neighborhood, B.Number, B.Street, B.StreetType, B.Uf, C.Document, C.AddressId, C.DateOfBirth, C.Email, C.Income, C.Name, C.PDFDocument, C.Phone, D.Id, D.City, D.Complement, D.CEP, D.Neighborhood, D.Number, D.Street, D.StreetType, D.Uf FROM Dependent A INNER JOIN Address B ON A.AddressId = B.Id INNER JOIN Customer C ON A.CustomerDocument = C.Document INNER JOIN Address D ON C.AddressId = D.Id WHERE A.Document = @DependentDocument ";
        public readonly static string GETALLGENERIC = " SELECT Document, CustomerDocument, Name, DateOfBirth, AddressId, Phone, Email FROM Dependent ";
        public readonly static string GETGENERIC = " SELECT Document, CustomerDocument, Name, DateOfBirth, AddressId, Phone, Email FROM Dependent WHERE Document = @Document ";

        public Customer Customer { get; set; }
        public Dependent()
        {
            
        }
        public Dependent(DependentDTO dependentDTO)
        {
            this.Document = dependentDTO.Document;
            this.Customer = new Customer { Document = dependentDTO.CustomerDocument };
            this.Name = dependentDTO.Name;
            this.DateOfBirth = dependentDTO.DateOfBirth;
            this.Phone = dependentDTO.Phone;
            this.Email = dependentDTO.Email;
        }
    }
}
