using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ConductorDTO
    {
        public string Document {  get; set; }
        public string Name {  get; set; }
        public DateTime DateOfBirth {  get; set; }
        public AddressDTO Address { get; set; }
        public string Phone {  get; set; }
        public string Email {  get; set; }
        public CNHDTO CNHDTO { get; set; }



    }
}
