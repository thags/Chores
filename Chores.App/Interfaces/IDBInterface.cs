using System;
using Chores.Models;

namespace Chores.Interfaces
{
    public interface IDBInterface
    {
        public void CreateTables();
        public void AddChore(Chore newDrive);
        public void EditChore(Chore editedDrive);
        public List<Chore> GetAllChores();
        public Chore GetChore(int id);
        public void DeleteChore(int id);
    }
}

