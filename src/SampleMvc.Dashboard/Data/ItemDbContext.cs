using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SampleMvc.Dashboard.Models;

namespace SampleMvc.Dashboard.Data
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}
