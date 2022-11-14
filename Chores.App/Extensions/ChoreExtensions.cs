using Chores.Models;

namespace Chores.Extensions
{
    public static class ChoreExtensions
    {
        public static Chore SetCompleteAndUpdateDueDate(this Chore chore)
        {
            chore.SetCompletionDate(DateTime.Now);
            chore.UpdateDueDate();
            return chore;
        }
        public static Chore UpdateDueDate(this Chore chore)
        {
            chore.NextDueDate = chore.CompletionDate.Add(chore.Recurrence);
            return chore;
        }

        private static Chore SetCompletionDate(this Chore chore, DateTime? completion = null)
        {
            chore.CompletionDate = (DateTime)(completion == null ? DateTime.Now : completion);
            return chore;
        }
    }
}
