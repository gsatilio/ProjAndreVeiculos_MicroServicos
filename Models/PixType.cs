using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PixType
    {
        public readonly static string INSERT = " INSERT INTO PIXTYPE (Name) VALUES (@Name); SELECT cast(scope_identity() as int) ";
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
