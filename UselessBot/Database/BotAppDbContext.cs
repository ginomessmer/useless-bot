using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UselessBot.Data;

namespace UselessBot.Database
{
    public class BotAppDbContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Gif> Gifs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
}
