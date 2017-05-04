using System;
using System.Collections.Generic;
using System.Linq;

namespace StartMvc.Models {
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context){
            _context = context;

            if(_context.TodoItems.Count() == 0){
                Add(new TodoItem{Name = "Item1"});
            }
        }

        public void Add(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }

        public TodoItem Find(string key)
        {
            return _context.TodoItems.FirstOrDefault(t => t.Key.ToUpper() == key.ToUpper());
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        public void Remove(string key)
        {
            var entity = _context.TodoItems.FirstOrDefault(t => t.Key == key);
            _context.TodoItems.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(TodoItem item)
        {
            _context.TodoItems.Update(item);
            _context.SaveChanges();
        }
    }
}