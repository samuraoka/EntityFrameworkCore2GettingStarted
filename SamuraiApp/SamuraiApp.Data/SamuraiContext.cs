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
    // Get-Help Add-Migration
    // Add-Migration -Name Initial -Context SamuraiContext -Project SamuraiApp.Data -StartupProject SomeCLI
    //
    // To generate a SQL script for making a database, run the following command.
    // Get-Help Script-Migration
    // Script-Migration -Idempotent -Context SamuraiContext -Project SamuraiApp.Data -StartupProject SomeCLI
    //
    // To generate a database, run the following command.
    // Get-Help Update-Database
    // Update-Database -Context SamuraiContext -Project SamuraiApp.Data -StartupProject SomeCLI -Verbose
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public SamuraiContext() : this(_defaultSettingFilePath)
        {
        }

        public SamuraiContext(string fileName)
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
