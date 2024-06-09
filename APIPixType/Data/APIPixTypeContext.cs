using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIPixType.Data
{
    public class APIPixTypeContext : DbContext
    {
        public APIPixTypeContext (DbContextOptions<APIPixTypeContext> options)
            : base(options)
        {
        }

        public DbSet<Models.PixType> PixType { get; set; } = default!;
    }
}
