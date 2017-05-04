using System.Collections.Generic;

namespace StartMvc.Models {
    public interface ITodoRepository{
        void Add(TodoItem item);
        IEnumerable<TodoItem> GetAll();
        TodoItem Find(string key);
        void Remove(string key);
        void Update(TodoItem item);
    }
}