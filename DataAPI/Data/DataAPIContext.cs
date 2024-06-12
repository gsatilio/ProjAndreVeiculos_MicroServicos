using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAPI.Data
{
    public class DataAPIContext : DbContext
    {
        public DataAPIContext(DbContextOptions<DataAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Acquisition> Acquisition { get; set; } = default!;
        public DbSet<Models.Address> Address { get; set; } = default!;
        public DbSet<Models.Bank> Bank { get; set; } = default!;
        public DbSet<Models.Boleto> Boleto { get; set; } = default!;
        public DbSet<Models.Car> Car { get; set; } = default!;
        public DbSet<Models.CarOperation> CarOperation { get; set; } = default!;
        public DbSet<Models.Category> Category { get; set; } = default!;
        public DbSet<Models.Conductor> Conductor { get; set; } = default!;
        public DbSet<Models.CreditCard> CreditCard { get; set; } = default!;
        public DbSet<Models.Customer> Customer { get; set; } = default!;
        public DbSet<Models.Dependent> Dependent { get; set; } = default!;
        public DbSet<Models.DriverLicense> DriverLicense { get; set; } = default!;
        public DbSet<Models.Employee> Employee { get; set; } = default!;
        public DbSet<Models.FinancialPending> FinancialPending { get; set; } = default!;
        public DbSet<Models.Financing> Financing { get; set; } = default!;
        public DbSet<Models.Insurance> Insurance { get; set; } = default!;
        public DbSet<Models.Operation> Operation { get; set; } = default!;
        public DbSet<Models.Payment> Payment { get; set; } = default!;
        public DbSet<Models.Pix> Pix { get; set; } = default!;
        public DbSet<Models.PixType> PixType { get; set; } = default!;
        public DbSet<Models.Role> Role { get; set; } = default!;
        public DbSet<Models.Sale> Sale { get; set; } = default!;
        public DbSet<Models.TermsOfUse> TermsOfUse { get; set; } = default!;
        public DbSet<Models.TermsOfUseAgreement> TermsOfUseAgreement { get; set; } = default!;
    }
}
