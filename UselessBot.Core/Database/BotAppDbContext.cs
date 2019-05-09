using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UselessBot.Core.Data;

namespace UselessBot.Core.Database
{
    public class BotAppDbContext : DbContext
    {
        private readonly string connectionString;

        public BotAppDbContext(string connectionString = "Data Source=app.db")
        {
            this.connectionString = connectionString;
            this.Database.EnsureCreated();
            this.Database.Migrate();
        }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Gif> Gifs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
