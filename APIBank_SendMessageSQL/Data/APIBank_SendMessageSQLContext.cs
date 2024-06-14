using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIBank_SendMessageSQL.Data
{
    public class APIBank_SendMessageSQLContext : DbContext
    {
        public APIBank_SendMessageSQLContext (DbContextOptions<APIBank_SendMessageSQLContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Bank> Bank { get; set; } = default!;
    }
}
