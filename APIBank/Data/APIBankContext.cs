using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIBank.Data
{
    public class APIBankContext : DbContext
    {
        public APIBankContext (DbContextOptions<APIBankContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Bank> Bank { get; set; } = default!;
    }
}
