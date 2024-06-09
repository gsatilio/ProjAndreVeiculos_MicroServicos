using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class EmployeeDTO
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Role Role { get; set; }
        public Decimal ComissionValue { get; set; }
        public Decimal Comission { get; set; }
        public AddressDTO Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
