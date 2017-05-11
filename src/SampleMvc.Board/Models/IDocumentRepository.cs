using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMvc.Board.Models
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> GetAll();

        IEnumerable<Document> Search(string searchKeyword = "");

        Document GetDocumentById(int id);

        void AddDocument(Document entity);

        void UpdateDocument(Document entity);

        void DeleteDocument(Document entity);

        void DeleteDocument(int id);
    }
}
