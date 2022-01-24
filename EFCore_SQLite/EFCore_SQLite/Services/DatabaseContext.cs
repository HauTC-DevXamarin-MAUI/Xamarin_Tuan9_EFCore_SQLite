using EFCore_SQLite.Helpers;
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
        public DbSet<Category> Categorys { get; set; }
        public IEnumerable<object> Students { get; internal set; }

        public DatabaseContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite($"Filename={Constant.DBPath}");

            //string dbPath1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            //    "item.db3");
            //Console.WriteLine(dbPath1);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne<Category>(d => d.Category)
                .WithMany(dm => dm.Items)
                .HasForeignKey(dkey => dkey.IdCategory);
        }
    }
}
