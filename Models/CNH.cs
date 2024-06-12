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
    public class CNH
    {
        public readonly static string INSERT = " INSERT INTO CNH (DriverLicense, DueDate, RG, CPF, MotherName, FatherName, CategoryId) VALUES (@DriverLicense, @DueDate, @RG, @CPF, @MotherName, @FatherName, @CategoryId) ";
        public readonly static string GETALL = " SELECT A.DriverLicense, A.DueDate, A.RG, A.CPF, A.MotherName, A.FatherName, B.Id, B.Description FROM CNH A INNER JOIN Category B ON A.CategoryId = B.Id ";
        public readonly static string GET = " SELECT A.DriverLicense, A.DueDate, A.RG, A.CPF, A.MotherName, A.FatherName, B.Id, B.Description FROM CNH A INNER JOIN Category B ON A.CategoryId = B.Id WHERE A.DriverLicense = @DriverLicense ";
        [Key]
        public long DriverLicense { get; set; }
        public DateTime DueDate { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public Category Category { get; set; }

        public CNH()
        {
            
        }

        public CNH(CNHDTO cnhDTO)
        {
            this.DriverLicense = cnhDTO.DriverLicense;
            this.DueDate = cnhDTO.DueDate;
            this.RG = cnhDTO.RG;
            this.CPF = cnhDTO.CPF;
            this.MotherName = cnhDTO.MotherName;
            this.FatherName = cnhDTO.FatherName;
            var category = new Category();
            this.Category = category;
        }
    }
}
