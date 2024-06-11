using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APICategory.Data
{
    public class APICategoryContext : DbContext
    {
        public APICategoryContext (DbContextOptions<APICategoryContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Category> Category { get; set; } = default!;
    }
}
