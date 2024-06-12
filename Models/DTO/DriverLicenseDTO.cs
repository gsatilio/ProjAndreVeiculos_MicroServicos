using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class DriverLicenseDTO
    {
        public long DriverId {  get; set; }
        public DateTime DueDate { get; set; }
        public string RG {  get; set; }
        public string CPF { get; set; }
        public string MotherName {  get; set; }
        public string FatherName {  get; set; }
        public CategoryDTO Category { get; set; }
    }
}
