using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APICarOperation.Data
{
    public class APICarOperationContext : DbContext
    {
        public APICarOperationContext (DbContextOptions<APICarOperationContext> options)
            : base(options)
        {
        }

        public DbSet<Models.CarOperation> CarOperation { get; set; } = default!;
    }
}
