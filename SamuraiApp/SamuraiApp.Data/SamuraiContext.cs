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
    // Next, Add-Migration command need to be executed in the executable project like SomeCLI.
    // So, install Microsoft.EntityFrameworkCore.Design package to the SomeCLI project
    //
    // Microsoft.EntityFrameworkCore.Tools
    // https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/
    // Install-Package -Id Microsoft.EntityFrameworkCore.Tools -ProjectName SamuraiApp.Data
    //
    // Microsoft.EntityFrameworkCore.Design
    // https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/
    // Install-Package -Id Microsoft.EntityFrameworkCore.Design -ProjectName SomeCLI
    //
    // Lastly you can execute the following command to create migration files.
    // Add-Migration -Name Initial -Context SamuraiContext -Project SamuraiApp.Data -StartupProject SomeCLI
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public string ConnectionString { get; set; }
            = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SamuraiAppData;Integrated Security=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
