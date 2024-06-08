using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Pix
    {
        public readonly static string INSERT = " INSERT INTO PIX (IdPixType, PixKey) VALUES (@IdPixType, @PixKey); SELECT cast(scope_identity() as int) ";
        public int Id { get; set; }
        public PixType PixType { get; set; }
        public string PixKey { get; set; }
    }
}
