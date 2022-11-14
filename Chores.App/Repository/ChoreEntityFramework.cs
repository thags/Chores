using Chores.Extensions;
using Chores.Interfaces;
using Chores.Models;
using Chores.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Chores.Repository
{
    public class ChoreEntityFramework : IChoreDB
    {
        private DataContext _choreDB;

        public ChoreEntityFramework(DataContext db)
        {
            _choreDB = db;
        }

        public IEnumerable<Chore> Get()
        {
            return (IEnumerable<Chore>)_choreDB.Chores;
        }

        [HttpGet("{id}")]
        public Chore GetById(int id)
        {
            return _choreDB.Find<Chore>(id);
        }

        [HttpDelete]
        public void Delete(Chore choreToRemove)
        {
            _choreDB.Remove(choreToRemove);
            _choreDB.SaveChanges();
        }

        [HttpDelete]
        public void DeleteById(int id)
        {
            Chore choreToDelete = GetById(id);
            if (choreToDelete != null)
            {
                _choreDB.Remove(choreToDelete);
                _choreDB.SaveChanges();
            }
        }

        [HttpPost]
        public void Add(Chore newChore)
        {
            newChore.UpdateDueDate();
            _choreDB.Add(newChore);
            _choreDB.SaveChanges();
        }

        [HttpPut]
        public void Update(Chore updatedChore)
        {
            updatedChore.UpdateDueDate();
            _choreDB.Update(updatedChore);
            _choreDB.SaveChanges();
        }
    }
}
