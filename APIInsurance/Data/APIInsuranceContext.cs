using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIInsurance.Data
{
    public class APIInsuranceContext : DbContext
    {
        public APIInsuranceContext (DbContextOptions<APIInsuranceContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Insurance> Insurance { get; set; } = default!;
    }
}
