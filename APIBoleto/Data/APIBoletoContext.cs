using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIBoleto.Data
{
    public class APIBoletoContext : DbContext
    {
        public APIBoletoContext (DbContextOptions<APIBoletoContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Boleto> Boleto { get; set; } = default!;
    }
}
