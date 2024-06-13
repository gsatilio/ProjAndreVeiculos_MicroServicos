using Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DriverLicense
    {
        public readonly static string INSERT = " INSERT INTO DriverLicense (DueDate, RG, CPF, MotherName, FatherName, CategoryId) VALUES (@DueDate, @RG, @CPF, @MotherName, @FatherName, @CategoryId); SELECT cast(scope_identity() as int)   ";
        public readonly static string GETALL = " SELECT A.DriverId, A.DueDate, A.RG, A.CPF, A.MotherName, A.FatherName, B.Id, B.Description FROM DriverLicense A INNER JOIN Category B ON A.CategoryId = B.Id ";
        public readonly static string GET = " SELECT A.DriverId, A.DueDate, A.RG, A.CPF, A.MotherName, A.FatherName, B.Id, B.Description FROM DriverLicense A INNER JOIN Category B ON A.CategoryId = B.Id WHERE A.DriverId = @DriverId ";
        [Key]
        public long DriverId { get; set; }
        public DateTime DueDate { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public Category Category { get; set; }


        public DriverLicense()
        {

        }

        public DriverLicense(DriverLicenseDTO cnhDTO)
        {
            this.DriverId = cnhDTO.DriverId;
            this.DueDate = cnhDTO.DueDate;
            this.RG = cnhDTO.RG;
            this.CPF = cnhDTO.CPF;
            this.MotherName = cnhDTO.MotherName;
            this.FatherName = cnhDTO.FatherName;
        }
    }
}
