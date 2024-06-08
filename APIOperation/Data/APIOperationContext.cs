using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIOperation.Data
{
    public class APIOperationContext : DbContext
    {
        public APIOperationContext (DbContextOptions<APIOperationContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Operation> Operation { get; set; } = default!;
    }
}
