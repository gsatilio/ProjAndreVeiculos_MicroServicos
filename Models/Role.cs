using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Role
    {
        public readonly static string INSERT = " INSERT INTO ROLE (Description) VALUES (@Description); SELECT cast(scope_identity() as int) ";
        public int Id { get; set; }
        public string Description { get; set; }

    }
}
