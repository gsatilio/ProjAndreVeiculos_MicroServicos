using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIDriver.Data
{
    public class APIDriverContext : DbContext
    {
        public APIDriverContext (DbContextOptions<APIDriverContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Category> Category { get; set; } = default!;

        public DbSet<Models.Conductor>? Conductor { get; set; }

        public DbSet<Models.DriverLicense>? DriverLicense { get; set; }
    }
}
