using SampleMvc.Board.Data;
using SampleMvc.Board.Models;
using SampleMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleMvc.Board.Data
{
    public class DocumentRepository : IDocumentRepository
    {
        private AppDbContext _context;
        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddDocument(Document entity)
        {
            _context.Documents.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteDocument(Document entity)
        {
            _context.Documents.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteDocument(int id)
        {
            var entity = _context.Documents.FirstOrDefault(d => d.Id == id);
            if (entity != null)
            {
                DeleteDocument(entity);
            }
        }

        public IEnumerable<Document> GetAll()
        {
            return _context.Documents;
        }

        public Document GetDocumentById(int id)
        {
            return _context.Documents.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<Document> Search(string searchKeyword)
        {
            var list = _context.Documents.AsEnumerable();

            if (!String.IsNullOrEmpty(searchKeyword))
            {
                list = list.Where(d => d.Title.ToUpper().Contains(searchKeyword.ToUpper()) || d.Content.ToUpper().Contains(searchKeyword.ToUpper()));
            }

            list = list.OrderByDescending(d => d.PostDate);

            return list;
        }

        public void UpdateDocument(Document entity)
        {
            if (_context.Documents.Any(d => d.Id == entity.Id))
            {
                _context.Documents.Update(entity);
                _context.SaveChanges();
            }
        }
    }
}
