using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;

namespace APIConductor.Data
{
    public class APIConductorContext : DbContext
    {
        public APIConductorContext (DbContextOptions<APIConductorContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Category> Category { get; set; } = default!;

        public DbSet<Models.CNH>? CNH { get; set; }

        public DbSet<Models.Conductor>? Conductor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Conductor>().ToTable("CNH").ToTable("Category");
        }   
    }
}
