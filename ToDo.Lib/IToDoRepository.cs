using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Lib
{
    public interface IToDoRepository
    {
        Task<ToDoItem> AddAsync(ToDoItem item);
        Task<IEnumerable<ToDoItem>> GetAllAsync(bool includeComplete = false);
        Task<ToDoItem> GetAsync(int id);
        Task<ToDoItem> UpdateAsync(ToDoItem item);
    }
}