using GBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GBank.Infrastructure.Persistence
{
    public class GBankDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        
        public GBankDbContext()
        {
        }

        public GBankDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            
            this.configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<BillTransactions> BillTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=192.168.5.3;Database=gabank;User Id=sa;Password=1qaz@WSX3edc;");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = new CancellationToken())
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is News && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((News)entityEntry.Entity).date = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((News)entityEntry.Entity).date = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync();
        }

        public static GBankDbContext Create()
        {
            return new GBankDbContext();
        }
    }
}
