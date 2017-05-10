using Microsoft.EntityFrameworkCore;
using SampleMvc.Board.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMvc.Board.Data
{
    public class DocumentDbContext : DbContext
    {
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
        {

        }

        public DbSet<Document> Documents { get; set; }

        
    }
}
