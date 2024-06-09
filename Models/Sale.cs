using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Sale
    {
        public readonly static string INSERT = " INSERT INTO SALE (LicensePlate, SaleDate, SaleValue, Customer, Employee, IdPayment) " +
            "VALUES (@LicensePlate, @SaleDate, @SaleValue, @Customer, @Employee, @IdPayment); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = "SELECT A.Id, A.SaleDate, A.SaleValue, " +
                                        "B.LicensePlate, B.Color, B.FabricationYear, B.ModelYear, B.Name, B.Sold," +
                                        "C.Document, C.DateOfBirth, C.Email, C.Income, C.Name, C.PDFDocument, C.Phone," +
                                        "S.Id, S.CEP, S.City, S.Complement, S.Neighborhood, S.Number, S.Street, S.StreetType, S.Uf," +
                                        "D.Document, D.Comission, D.ComissionValue, D.DateOfBirth, D.Email, D.Name, D.Phone," +
                                        "T.Id, T.CEP, T.City, T.Complement, T.Neighborhood, T.Number, T.Street, T.StreetType, T.Uf," +
                                        "R.Id, R.Description," +
                                        "E.Id, E.PaymentDate, " +
                                        "F.Id, F.CardName, F.CardNumber, F.ExpirationDate, F.SecurityCode, " +
                                        "G.Id, G.ExpirationDate, G.Number, " +
                                        "H.Id, H.PixKey, " +
                                        "I.Id, I.Name " +
                                        "FROM Sale A " +
                                        "INNER JOIN Car B ON A.CarLicensePlate = B.LicensePlate " +
                                        "INNER JOIN Customer C ON A.CustomerDocument = C.Document " +
                                        "INNER JOIN Employee D ON A.EmployeeDocument = D.Document " +
                                        "INNER JOIN Payment E ON A.PaymentId = E.Id " +
                                        "INNER JOIN CreditCard F ON E.CreditCardId = F.Id " +
                                        "INNER JOIN Boleto G ON E.BoletoId = G.Id " +
                                        "INNER JOIN Pix H ON E.PixId = H.Id " +
                                        "INNER JOIN PixType I ON H.PixTypeId = I.Id " +
                                        "INNER JOIN Role R ON R.Id = D.RoleId " +
                                        "INNER JOIN Address S ON S.Id = C.AddressId " +
                                        "INNER JOIN Address T ON T.Id = D.AddressId ";
        public readonly static string GET = GETALL + " WHERE A.Id = @IdSale";
        public readonly static string GETALLGENERIC = "SELECT Id, CarLicensePlate, SaleDate, SaleValue, CustomerDocument, EmployeeDocument, PaymentId FROM Sale ";
        public readonly static string GETGENERIC = GETALLGENERIC + " WHERE Id = @IdSale";

        public int Id { get; set; }
        public Car Car { get; set; }
        public DateTime SaleDate { get; set; }
        public Decimal SaleValue { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Payment Payment { get; set; }

    }
}
