﻿using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    // Microsoft.EntityFrameworkCore.SqlServer
    // https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/
    // Install-Package -Id Microsoft.EntityFrameworkCore.SqlServer -ProjectName SamuraiApp.Data
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }
    }
}
 