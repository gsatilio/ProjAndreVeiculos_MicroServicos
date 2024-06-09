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

        public readonly static string GETALL = " SELECT Id, Description FROM Role ";
        public readonly static string GET = " SELECT Id, Description FROM Role WHERE Id = @IdRole";

        public int Id { get; set; }
        public string Description { get; set; }

    }
}
