using System;
using Chores.Models;

namespace Chores.Interfaces
{
    public class IDBInterface
    {
        public interface IChoreDB
        {
            public void CreateTables();
            public void AddChore(Chore newDrive);
            public void EditChore(Chore editedDrive);
            public List<Chore> GetAllChores();
            public void DeleteChore(int id);
        }
    }
}

