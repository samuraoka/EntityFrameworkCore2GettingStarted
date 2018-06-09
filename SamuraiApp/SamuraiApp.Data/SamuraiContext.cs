using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    // Microsoft.EntityFrameworkCore.SqlServer
    // https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/
    // Install-Package -Id Microsoft.EntityFrameworkCore.SqlServer -ProjectName SamuraiApp.Data
    //
    // To create migration file, it requires Microsoft.EntityFrameworkCore.Tools package.
    // Run the "get-help entityframeworkcore" command in the Package Manager Console to get
    // a help message how to create migration file.
    // Microsoft.EntityFrameworkCore.Tools
    // https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/
    // Install-Package -Id Microsoft.EntityFrameworkCore.Tools -ProjectName SamuraiApp.Data
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public SamuraiContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(_connectionString);
                base.OnConfiguring(optionsBuilder);
            }
        }

        private readonly string _connectionString;
    }
}
