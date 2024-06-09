using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIRole.Data
{
    public class APIRoleContext : DbContext
    {
        public APIRoleContext (DbContextOptions<APIRoleContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Role> Role { get; set; } = default!;
    }
}
