using SampleMvc.Board.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleMvc.Board.Models
{
    public class DocumentRepository : IDocumentRepository
    {
        private DocumentDbContext _context;
        public DocumentRepository(DocumentDbContext context)
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

        public IEnumerable<Document> Search(int page, string searchKeyword)
        {
            return _context.Documents.Where(d => d.Title.ToUpper().Contains(searchKeyword) || d.Content.ToUpper().Contains(searchKeyword)).OrderByDescending(d => d.Id);
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
