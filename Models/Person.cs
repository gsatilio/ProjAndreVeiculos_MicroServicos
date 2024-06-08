using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Person
    {
        [Key]
        public string Document { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Address Address { get; set; }

        public string Phone   { get; set; }

        public string Email { get; set; }

    }
}
