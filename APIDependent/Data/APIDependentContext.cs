using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIDependent.Data
{
    public class APIDependentContext : DbContext
    {
        public APIDependentContext (DbContextOptions<APIDependentContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Dependent> Dependent { get; set; } = default!;
    }
}
