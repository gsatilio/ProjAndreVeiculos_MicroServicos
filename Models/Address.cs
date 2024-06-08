using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Address
    {
        public readonly static string INSERT = " INSERT INTO Address (Street, CEP, Neighborhood, StreetType, Complement, Number, Uf, City) " +
            "VALUES (@Street, @CEP, @Neighborhood, @StreetType, @Complement, @Number, @Uf, @City); SELECT cast(scope_identity() as int) ";
        public int Id { get; set; }
        public  string Street { get; set; }
        public string CEP { get; set; }
        public string Neighborhood { get; set; }
        public string StreetType { get; set; }
        public string Complement { get; set; }
        public int Number { get; set; }
        public string Uf { get; set; }
        public string City { get; set; }
    }
}
