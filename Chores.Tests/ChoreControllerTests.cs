using Chores.Interfaces;
using Chores.Models;
using Chores.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Chores.Tests
{
    public class ChoreControllerTests
    {
        private readonly Mock<ILogger<ChoreController>> _logger = new();

        [Test]
        public void Should_Call_Db_Interface()
        {
            // Arrange
            Chore testChore = new Chore
            {
                Id = 3,
                Name = "Adding test chore",
                Note = "Delete me, please!",
                CompletionDate = DateTime.Today
            };

            Mock<IDBInterface> _db = new();

            _db
                .Setup(m => m.GetAllChores());

            ChoreController choreCon = new(_logger.Object, _db.Object);

            // Act
            choreCon.Get();
            choreCon.Update(testChore);
            choreCon.Create(testChore);
            choreCon.Delete(1);

            // Assert
            Assert.That(_db.Invocations.Count, Is.EqualTo(4));
        }
    }
}