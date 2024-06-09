using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIPix.Data
{
    public class APIPixContext : DbContext
    {
        public APIPixContext (DbContextOptions<APIPixContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Pix> Pix { get; set; } = default!;
    }
}
