using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIAcquisition.Data
{
    public class APIAcquisitionContext : DbContext
    {
        public APIAcquisitionContext (DbContextOptions<APIAcquisitionContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Acquisition> Acquisition { get; set; } = default!;
    }
}
