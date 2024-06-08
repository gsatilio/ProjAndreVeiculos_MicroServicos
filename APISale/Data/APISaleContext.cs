using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APISale.Data
{
    public class APISaleContext : DbContext
    {
        public APISaleContext (DbContextOptions<APISaleContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Sale> Sale { get; set; } = default!;
    }
}
