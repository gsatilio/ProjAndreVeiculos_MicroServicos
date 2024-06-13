using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Category
    {
        public readonly static string INSERT = " INSERT INTO Category (Description) VALUES (@Description); SELECT cast(scope_identity() as int)  "; 
        public readonly static string GETALL = " SELECT Id, Description FROM Category ";
        public readonly static string GET = " SELECT Id, Description FROM Category WHERE Id = @IdCategory ";
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
