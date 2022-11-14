using Chores.Controllers;
using Chores.Interfaces;
using Moq;
using NUnit.Framework;
using Chores.Extensions;
using Chores.Models;

namespace Chores.Tests
{
    public class ChoreExtensionsTests
    {
        [Test]
        public void Should_Update_Due_Date()
        {
            // Arrange
            DateTime completionDate = DateTime.Parse("11/14/2022");
            TimeSpan recurrence = TimeSpan.FromDays(7);
            DateTime expectedNextDueDate = DateTime.Parse("11/21/2022");
            
            Chore testChore = new Chore
            {
                Id = 1,
                Name = "test",
                Note = "Extension test",
                CompletionDate = completionDate,
                NextDueDate = DateTime.Today,
                Recurrence = recurrence
            };

            // Act
            testChore.UpdateDueDate();

            // Assert
            Assert.That(expectedNextDueDate, Is.EqualTo(testChore.NextDueDate));
        }

        public void Should_Set_Complete_Date_And_Update_Next_Due_Date()
        {
            // Arrange
            DateTime completionDate = DateTime.Parse("11/14/2022");
            TimeSpan recurrence = TimeSpan.FromDays(7);
            DateTime expectedNextDueDate = DateTime.Parse("11/21/2022");

            Chore testChore = new Chore
            {
                Id = 1,
                Name = "test",
                Note = "Extension test",
                CompletionDate = DateTime.MinValue,
                NextDueDate = DateTime.MaxValue,
                Recurrence = recurrence
            };

            // Act
            testChore.SetCompleteAndUpdateDueDate(completionDate);

            // Assert
            Assert.That(expectedNextDueDate, Is.EqualTo(testChore.NextDueDate));
        }
    }
}
