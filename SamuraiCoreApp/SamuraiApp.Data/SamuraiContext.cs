using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace SamuraiApp.Data
{
    // Microsoft.EntityFrameworkCore.SqlServer
    // https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/
    // Get-Help Install-Package
    // Install-Package -Id Microsoft.EntityFrameworkCore.SqlServer -ProjectName SamuraiApp.Data -Version 2.0.3
    //
    // Execute the following command to create migration files.
    // Get-Help Add-Migration
    // Add-Migration -Name Initial -Context SamuraiContext -Project SamuraiApp.Data -StartupProject SamuraiApp.Web
    // Add-Migration -Name Relationship -Context SamuraiContext -Project SamuraiApp.Data -StartupProject SamuraiApp.Web
    //
    // To generate a SQL script for making a database, run the following command.
    // Get-Help Script-Migration
    // Script-Migration -Idempotent -Context SamuraiContext -Project SamuraiApp.Data -StartupProject SamuraiApp.Web
    //
    // To generate a database, run the following command.
    // Get-Help Update-Database
    // Update-Database -Context SamuraiContext -Project SamuraiApp.Data -StartupProject SamuraiApp.Web
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public SamuraiContext(DbContextOptions<SamuraiContext> options) : this(options, _defaultSettingFilePath)
        {
        }

        public SamuraiContext(DbContextOptions<SamuraiContext> options, string fileName) : base(options)
        {
            _connectionString = ReadConnectionStringFrom(fileName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(_connectionString);
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table Mapping
            // https://docs.microsoft.com/en-us/ef/core/modeling/relational/tables
            modelBuilder.Entity<Samurai>().ToTable("Samurai");
            modelBuilder.Entity<Quote>().ToTable("Quote");
            modelBuilder.Entity<Battle>().ToTable("Battle");
            modelBuilder.Entity<SamuraiBattle>().ToTable("SamuraiBattle");

            // Keys (primary)
            // https://docs.microsoft.com/en-us/ef/core/modeling/keys
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.SamuraiId, s.BattleId });
        }

        private const string _defaultSettingFilePath = "connectionString.json";
        private readonly string _connectionString;

        /// <summary>
        /// Read a connection string from a file.
        /// </summary>
        /// <param name="fileName">file name of json format</param>
        /// <returns>connection string</returns>
        private string ReadConnectionStringFrom(string fileName)
        {
            var connectionString = string.Empty;
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);

                // Newtonsoft.Json
                // https://www.nuget.org/packages/Newtonsoft.Json/
                // Install-Package -Id Newtonsoft.Json -ProjectName SamuraiApp.Data
                //
                // How can I deserialize JSON to a simple Dictionary<string,string> in ASP.NET?
                // https://stackoverflow.com/questions/1207731/how-can-i-deserialize-json-to-a-simple-dictionarystring-string-in-asp-net
                var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                try
                {
                    connectionString = dic["connectionString"];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            return connectionString;
        }
    }
}
