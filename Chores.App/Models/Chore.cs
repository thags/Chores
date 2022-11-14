using System;
namespace Chores.Models
{
    public class Chore
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Note { get; set; }

        public DateTime CompletionDate { get; set; }

        public DateTime? NextDueDate { get; set; }

        public TimeSpan Recurrence { get; set; }
    }
}