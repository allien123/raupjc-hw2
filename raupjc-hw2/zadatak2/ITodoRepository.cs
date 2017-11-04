using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadatak2
{
    public interface ITodoRepository
    {
        /// <summary >
        /// Gets TodoItem for a given id
        /// </ summary >
        /// <returns > TodoItem if found , null otherwise </ returns >
        TodoItem Get(Guid todoId);

        /// <summary >
        /// Adds new TodoItem object in database .
        /// If object with the same id already exists ,
        /// method should throw DuplicateTodoItemException with the message " duplicate id: {id }".
        /// </ summary >
        /// <returns > TodoItem that was added </ returns >
        TodoItem Add(TodoItem todoItem);

        /// <summary >
        /// Tries to remove a TodoItem with given id from the database .
        /// </ summary >
        /// <returns > True if success , false otherwise </ returns >
        bool Remove(Guid todoId);

        /// <summary >
        /// Updates given TodoItem in the database .
        /// If TodoItem does not exist , method will add one .
        /// </ summary >
        /// <returns > TodoItem that was updated </ returns >
        TodoItem Update(TodoItem todoItem);

        /// <summary >
        /// Tries to mark a TodoItem as completed in the database .
        /// </ summary >
        /// <returns > True if success , false otherwise </ returns >
        bool MarkAsCompleted(Guid todoId);

        /// <summary >
        /// Gets all TodoItem objects in the database , sorted by date created (descending )
        /// </ summary >
        List<TodoItem> GetAll();

        /// <summary >
        /// Gets all incomplete TodoItem objects in the database
        /// </ summary >
        List<TodoItem> GetActive();

        /// <summary >
        /// /// Gets all completed TodoItem objects in the database
        /// </ summary >
        List<TodoItem> GetCompleted();
        /// <summary >
        /// Gets all TodoItem objects in database that apply to the filter
        /// </ summary >
        List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction);

    }
}
