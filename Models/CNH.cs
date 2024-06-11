using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CNH
    {
        [Key]
        public long DriverLicense { get; set; }
        public DateTime DueDate { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public Category Category { get; set; }
    }
}
