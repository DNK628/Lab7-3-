using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab7_3_.Models;
using Microsoft.EntityFrameworkCore;


namespace Lab7_3_.Data

{
    public class AppDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(System.AppContext.BaseDirectory, "restaurants.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}