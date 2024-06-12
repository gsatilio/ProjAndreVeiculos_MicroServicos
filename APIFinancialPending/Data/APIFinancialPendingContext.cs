using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIFinancialPending.Data
{
    public class APIFinancialPendingContext : DbContext
    {
        public APIFinancialPendingContext (DbContextOptions<APIFinancialPendingContext> options)
            : base(options)
        {
        }

        public DbSet<Models.FinancialPending> FinancialPending { get; set; } = default!;
    }
}
