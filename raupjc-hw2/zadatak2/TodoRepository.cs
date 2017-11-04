using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zadatak2.collections;

namespace zadatak2
{
    /// <summary >
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </ summary >
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;
        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new GenericList<TodoItem>();
            }
            // Shorter way to write this in C# using ?? operator :
            // x ?? y = > if x is not null , expression returns x. Else it will return y.
            // _inMemoryTodoDatabase = initialDbState ?? new List < TodoItem >();
        }

        public TodoItem Get(Guid todoId)
        {
            return _inMemoryTodoDatabase.Where(item=>item.Id == todoId).FirstOrDefault();
        }

        public TodoItem Add(TodoItem todoItem)
        {
            if (_inMemoryTodoDatabase.Contains(todoItem))
            {
                throw new DuplicateTodoItemException(todoItem.Id);
            }
            _inMemoryTodoDatabase.Add(todoItem);
            return todoItem;
        }

        public bool Remove(Guid todoId)
        {
            return _inMemoryTodoDatabase.Remove(Get(todoId));
        }

        public TodoItem Update(TodoItem todoItem)
        {
            _inMemoryTodoDatabase.Remove(todoItem);
            Add(todoItem);
            return todoItem;
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            TodoItem item = Get(todoId);
            return item==null?false:item.MarkAsCompleted();
        }

        public List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.OrderByDescending(item=>item.DateCreated).ToList();
        }

        public List<TodoItem> GetActive()
        {
            return _inMemoryTodoDatabase.Where(item=>!item.IsCompleted).ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return _inMemoryTodoDatabase.Where(item => item.IsCompleted).ToList(); 
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return _inMemoryTodoDatabase.Where(filterFunction).ToList();
        }
    }

}
