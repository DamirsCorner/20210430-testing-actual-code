using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;
using ToDo.Lib;

namespace ToDo.Tests
{
    public class IntegrationTests
    {
        [Test]
        public async Task CompleteAsyncMarksItemAsComplete()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<ToDoContext>(opts => opts.UseInMemoryDatabase("ToDoDb"))
                .AddTransient<IToDoRepository, ToDoRepository>()
                .AddTransient<ToDoService>()
                .BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ToDoService>();
            var item = await service.CreateAsync("Write test");

            var result = await service.CompleteAsync(item.Id);

            Assert.That(item.Completed, Is.True);
        }
    }
}
