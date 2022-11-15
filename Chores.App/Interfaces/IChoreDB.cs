using Chores.Models;

namespace Chores.Interfaces
{
    public interface IChoreDB
    {
        public IEnumerable<Chore> Get();
        public Chore GetById(int id);
        public void Delete(Chore choreToRemove);
        public void DeleteById(int id);
        public void Add(Chore newChore);
        public void Update(Chore updatedChore);
    }
}
