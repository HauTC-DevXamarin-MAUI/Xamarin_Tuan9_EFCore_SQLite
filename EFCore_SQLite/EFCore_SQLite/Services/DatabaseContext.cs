using EFCore_SQLite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace EFCore_SQLite.Services
{
    class DatabaseContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public DatabaseContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "item.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
