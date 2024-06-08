using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIEmployee.Data
{
    public class APIEmployeeContext : DbContext
    {
        public APIEmployeeContext (DbContextOptions<APIEmployeeContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Employee> Employee { get; set; } = default!;
    }
}
