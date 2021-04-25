using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Lib
{
    public class ToDoService
    {
        private readonly IToDoRepository repository;

        public ToDoService(IToDoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ToDoItem> CreateAsync(string label)
        {
            var item = new ToDoItem
            {
                Label = label,
            };

            return await this.repository.AddAsync(item);
        }

        public async Task<ToDoItem> CompleteAsync(int id)
        {
            var item = await this.repository.GetAsync(id);

            if (item == null)
            {
                throw new ArgumentException($"No item with id {id}.", nameof(id));
            }

            item.Completed = true;
            return await this.repository.UpdateAsync(item);
        }

        public async Task<IEnumerable<ToDoItem>> GetIncompleteAsync()
        {
            return await this.repository.GetAllAsync();
        }
    }
}
