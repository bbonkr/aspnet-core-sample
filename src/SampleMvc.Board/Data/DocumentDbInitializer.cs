using SampleMvc.Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleMvc.Board.Data
{
    public class DocumentDbInitializer
    {
        public static void Initialize(DocumentDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Documents.Any() )
            {
                // DB has been seed
                return;
            }

            var documents = new Document[] {
                new Document
                {
                    Title="Test 1",
                    Content = "Hello, World!",
                    Name = "Tester",

                },
                new Document
                {
                    Title="Test 2",
                    Content = "Hello, World!",
                    Name = "Tester",

                },
            };

            context.Documents.AddRange(documents);
            context.SaveChanges();
        }
    }
}
