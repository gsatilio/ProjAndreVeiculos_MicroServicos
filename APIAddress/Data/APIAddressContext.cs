using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIAddress.Data
{
    public class APIAddressContext : DbContext
    {
        public APIAddressContext (DbContextOptions<APIAddressContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Address> Address { get; set; } = default!;
    }
}
