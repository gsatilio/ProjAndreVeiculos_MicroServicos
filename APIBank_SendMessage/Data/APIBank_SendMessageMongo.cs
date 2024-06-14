using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIBank_SendMessage.Data
{
    public class APIBank_SendMessageMongo : DbContext
    {
        public APIBank_SendMessageMongo (DbContextOptions<APIBank_SendMessageMongo> options)
            : base(options)
        {
        }

        public DbSet<Models.Bank> Bank { get; set; } = default!;
    }
}
