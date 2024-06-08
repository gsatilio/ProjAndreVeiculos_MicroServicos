using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class AddressDTO
    {
        //public int Id { get; set; }
        public string CEP { get; set; }
        public string StreetType { get; set; }
        public string Complement { get; set; }
        public int Number { get; set; }
    }
}
