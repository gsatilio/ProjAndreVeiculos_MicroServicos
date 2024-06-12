using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Conductor : Person
    {

        public readonly static string INSERT = " INSERT INTO Conductor (Document, CNHDriverLicense, Name, DateOfBirth, AddressId, Phone, Email) VALUES (@Document, @CNHDriverLicense, @Name, @DateOfBirth, @AddressId, @Phone, @Email) ";
        public readonly static string GETALL = " SELECT A.Document, A.Name, A.DateOfBirth, A.Phone, A.Email, B.DriverLicense, B.DueDate, B.RG, B.CPF, B.MotherName, B.FatherName, C.Id, C.Description, D.Id, D.CEP, D.City, D.Complement, D.Neighborhood, D.Number, D.Street, D.StreetType, D.Uf FROM Conductor A INNER JOIN CNH B ON A.DriverLicense = B.DriverLicense INNER JOIN Category C ON B.CategoryId = C.Id INNER JOIN Address D ON A.AddressId = D.Id ";
        public readonly static string GET = " SELECT A.Document, A.Name, A.DateOfBirth, A.Phone, A.Email, B.DriverLicense, B.DueDate, B.RG, B.CPF, B.MotherName, B.FatherName, C.Id, C.Description, D.Id, D.CEP, D.City, D.Complement, D.Neighborhood, D.Number, D.Street, D.StreetType, D.Uf FROM Conductor A INNER JOIN CNH B ON A.DriverLicense = B.DriverLicense INNER JOIN Category C ON B.CategoryId = C.Id INNER JOIN Address D ON A.AddressId = D.Id WHERE Document = @Document";
        public DriverLicense DriverLicense { get; set; }
    }
}
