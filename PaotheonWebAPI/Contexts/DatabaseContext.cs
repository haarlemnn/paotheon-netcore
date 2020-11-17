using PaotheonWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaotheonWebAPI.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Bakery> Bakery { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=paotheon;Username=docker;Password=docker", db => db.UseNetTopologySuite());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
        }
    }
}
