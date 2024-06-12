using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIFinancing.Data
{
    public class APIFinancingContext : DbContext
    {
        public APIFinancingContext (DbContextOptions<APIFinancingContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Financing> Financing { get; set; } = default!;
    }
}
