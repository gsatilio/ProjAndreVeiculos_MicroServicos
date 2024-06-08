using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIPayment.Data
{
    public class APIPaymentContext : DbContext
    {
        public APIPaymentContext (DbContextOptions<APIPaymentContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Payment> Payment { get; set; } = default!;
    }
}
