using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APITermsOfUse.Data
{
    public class APITermsOfUseContext : DbContext
    {
        public APITermsOfUseContext (DbContextOptions<APITermsOfUseContext> options)
            : base(options)
        {
        }

        public DbSet<Models.TermsOfUse> TermsOfUse { get; set; } = default!;

        public DbSet<Models.TermsOfUseAgreement>? TermsOfUseAgreement { get; set; }
    }
}
