using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIBank_Mongo.Data
{
    public class APIBank_MongoContext : DbContext
    {
        public APIBank_MongoContext (DbContextOptions<APIBank_MongoContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Bank> Bank { get; set; } = default!;
    }
}
