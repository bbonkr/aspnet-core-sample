using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SampleMvc.Board.Models;

namespace SampleMvc.Data
{
    public class AppDbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Documents.Any())
            {
                // DB has been seed
                return;
            }

            //var documents = new Document[] {
            //    new Document
            //    {
            //        Title="Test 1",
            //        Content = "Hello, World!",
            //        Name = "Tester",

            //    },
            //    new Document
            //    {
            //        Title="Test 2",
            //        Content = "Hello, World!",
            //        Name = "Tester",

            //    },
            //};

            var documents = new List<Document>();

            for (int i = 0; i < 2000; i++)
            {
                documents.Add(new Document
                {
                    Title = $"Test Document {i + 1}",
                    Content = "Hello world!!",
                    Name = "Tester"
                });
            }

            context.Documents.AddRange(documents);
            context.SaveChanges();
        }
    }
}
