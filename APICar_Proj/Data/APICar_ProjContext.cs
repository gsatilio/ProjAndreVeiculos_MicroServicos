using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APICar.Data
{
    public class APICar_ProjContext : DbContext
    {
        public APICar_ProjContext (DbContextOptions<APICar_ProjContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Car> Car { get; set; } = default!;
    }
}
