using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Operation
    {
        public readonly static string INSERT = " INSERT INTO Operation (Description) VALUES (@Description); SELECT cast(scope_identity() as int) ";
        public readonly static string GETALL = " SELECT Id, Description FROM Operation ";
        public readonly static string GET = " SELECT Id, Description FROM Operation WHERE Id = @IdOperation";
        public int Id { get; set; }
        public string Description { get; set; }
        

        public override string ToString()
        {
            return $"Id: {Id}, Descrição: {Description}";
        }
    }
}
