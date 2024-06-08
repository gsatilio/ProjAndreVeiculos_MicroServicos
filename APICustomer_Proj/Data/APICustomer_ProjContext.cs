using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APICustomer.Data
{
    public class APICustomer_ProjContext : DbContext
    {
        public APICustomer_ProjContext (DbContextOptions<APICustomer_ProjContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Customer> Customer { get; set; } = default!;
    }
}
