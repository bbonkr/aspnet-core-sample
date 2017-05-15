using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SampleMvc.Dashboard.Models;

namespace SampleMvc.Dashboard.Data
{
    public partial class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}
