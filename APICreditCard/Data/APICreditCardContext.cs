using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APICreditCard.Data
{
    public class APICreditCardContext : DbContext
    {
        public APICreditCardContext (DbContextOptions<APICreditCardContext> options)
            : base(options)
        {
        }

        public DbSet<Models.CreditCard> CreditCard { get; set; } = default!;
    }
}
