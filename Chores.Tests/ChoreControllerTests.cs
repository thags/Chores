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
        private Chore _testChore = new Chore
        {
            Id = 3,
            Name = "Adding test chore",
            Note = "Delete me, please!",
            CompletionDate = DateTime.Today,
            NextDueDate = DateTime.Today.AddDays(-1),
            Recurrence = TimeSpan.FromDays(7)
        };

        [Test]
        public void Should_Call_Db_Interface()
        {
            // Arrange
            Mock<IDBInterface> _db = new();

            _db
                .Setup(m => m.GetAllChores());

            ChoreController choreCon = new(_logger.Object, _db.Object);

            // Act
            choreCon.Get();
            choreCon.Update(_testChore);
            choreCon.Add(_testChore);
            choreCon.Delete(1);

            // Assert
            Assert.That(_db.Invocations.Count, Is.EqualTo(4));
        }

        [Test]
        public void Should_Create_Due_Date_On_Chore_Add()
        {
            // Arrange
            Mock<IDBInterface> _db = new();
            Chore completedChore = new Chore();

            _db
                .Setup(m => m.AddChore(_testChore))
                .Callback((Chore x) => completedChore = x);

            ChoreController choreCon = new(_logger.Object, _db.Object);

            // Act
            choreCon.Add(_testChore);

            // Assert
            Assert.That(completedChore.NextDueDate?.Date, Is.EqualTo(DateTime.Now.AddDays(7).Date));
        }

        [Test]
        public void Should_Update_Due_Date_On_Chore_Edit()
        {
            // Arrange
            Mock<IDBInterface> _db = new();
            Chore completedChore = new Chore();

            _db
                .Setup(m => m.EditChore(_testChore))
                .Callback((Chore x) => completedChore = x);

            ChoreController choreCon = new(_logger.Object, _db.Object);

            // Act
            choreCon.Update(_testChore);

            // Assert
            Assert.That(completedChore.NextDueDate?.Date, Is.EqualTo(DateTime.Now.AddDays(7).Date));
        }
    }
}