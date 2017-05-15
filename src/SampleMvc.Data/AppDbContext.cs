using System;
using Microsoft.EntityFrameworkCore;

namespace SampleMvc.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
