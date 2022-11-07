using Chores.Interfaces;
using Chores.Models;
using Chores.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Chores.Tests
{
    public class ChoreControllerTests
    {

        private readonly Mock<ILogger<ChoreController>> _logger = new();
        private Mock<IDBInterface> _db = new();
        private List<Chore> _testChores = new List<Chore> {
                new Chore
                {
                    Id = 1,
                    Name = "Moq chore 1",
                    Note = "for moq only",
                    CompletionDate = DateTime.Now
                },
                new Chore
                {
                    Id = 2,
                    Name = "Moq chore 2",
                    Note = "for moq only",
                    CompletionDate = DateTime.Now
                }
            };

        [Test]
        public void Should_Get_All_Chores()
        {
            // Arrange
            _db
                .Setup(m => m.GetAllChores())
                .Returns(_testChores);

            ChoreController choreCon = new(_logger.Object, _db.Object);

            // Act
            List<Chore> returnedChores = choreCon.Get();

            // Assert
            Assert.That(returnedChores, Is.EqualTo(_testChores));

        }
    }
}