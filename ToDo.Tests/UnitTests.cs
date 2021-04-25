using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using ToDo.Lib;

namespace ToDo.Tests
{
    public class UnitTests
    {
        [Test]
        public async Task CompleteAsyncWorks()
        {
            var repositoryMock = new Mock<IToDoRepository>();
            var service = new ToDoService(repositoryMock.Object);

            var incompleteItem = new ToDoItem
            {
                Id = 1,
                Label = "Write test",
                Completed = false,
            };

            var completeItem = new ToDoItem
            {
                Id = incompleteItem.Id,
                Label = incompleteItem.Label,
                Completed = true,
            };

            repositoryMock
                .Setup(m => m.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(incompleteItem);
            repositoryMock
                .Setup(m => m.UpdateAsync(It.IsAny<ToDoItem>()))
                .ReturnsAsync(completeItem);

            var result = await service.CompleteAsync(incompleteItem.Id);

            Assert.That(result.Completed, Is.True);
        }

        [Test]
        public async Task CompleteAsyncMarksItemAsComplete()
        {
            var repositoryMock = new Mock<IToDoRepository>();
            var service = new ToDoService(repositoryMock.Object);

            var item = new ToDoItem
            {
                Id = 1,
                Label = "Write test",
                Completed = false,
            };

            repositoryMock
                .Setup(m => m.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(item);
            repositoryMock
                .Setup(m => m.UpdateAsync(It.IsAny<ToDoItem>()))
                .Returns<ToDoItem>(i => Task.FromResult(i));

            var result = await service.CompleteAsync(item.Id);

            Assert.That(item.Completed, Is.True);
        }
    }
}