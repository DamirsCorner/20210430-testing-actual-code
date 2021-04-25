using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Lib
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoContext context;

        public ToDoRepository(ToDoContext context)
        {
            this.context = context;
        }

        public async Task<ToDoItem> AddAsync(ToDoItem item)
        {
            this.context.ToDoItems.Add(item);
            await this.context.SaveChangesAsync();
            return item;
        }

        public async Task<ToDoItem> UpdateAsync(ToDoItem item)
        {
            this.context.ToDoItems.Update(item);
            await this.context.SaveChangesAsync();
            return item;
        }

        public async Task<ToDoItem> GetAsync(int id)
        {
            return await this.context.ToDoItems
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync(bool includeComplete = false)
        {
            return await this.context.ToDoItems
                .Where(item => includeComplete || !item.Completed)
                .ToListAsync();
        }
    }
}
