using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SampleMvc.Board.Models;

namespace SampleMvc.Data
{
    public partial class AppDbContext
    {
        public DbSet<Document> Documents { get; set; }
    }
}
