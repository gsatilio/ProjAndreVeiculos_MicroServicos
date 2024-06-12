using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Acquisition
    {
        public readonly static string INSERT = " INSERT INTO Acquisition (CarLicensePlate, Price, AcquisitionDate) VALUES (@CarLicensePlate, @Price, @AcquisitionDate); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT A.Id, A.Price, A.AcquisitionDate, B.LicensePlate, B.Name, B.ModelYear, B.FabricationYear, B.Color, B.Sold FROM Acquisition A INNER JOIN Car B ON A.CarLicensePlate = B.LicensePlate ";
        public readonly static string GET = " SELECT A.Id, A.Price, A.AcquisitionDate, B.LicensePlate, B.Name, B.ModelYear, B.FabricationYear, B.Color, B.Sold FROM Acquisition A INNER JOIN Car B ON A.CarLicensePlate = B.LicensePlate WHERE A.Id = @IdAcquisition ";

        public int Id { get; set; }
        public Car Car { get; set; }
        public Decimal Price { get; set; }
        public DateTime AcquisitionDate { get; set; }

    }
}