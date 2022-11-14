using Chores.Interfaces;
using Chores.Models;
using Chores.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Chores.Controllers;

[ApiController]
[Route("[controller]")]
public class ChoreController : ControllerBase
{
    private DataContext _choreDB;

    private readonly ILogger<ChoreController> _logger;

    public ChoreController(ILogger<ChoreController> logger, DataContext db)
    {
        _logger = logger;
        _choreDB = db;
    }

    [HttpGet]
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

    [HttpPost]
    public void Add(Chore newChore)
    {
        
        newChore = UpdateNextDueDate(newChore);
        _choreDB.Add(newChore);
        _choreDB.SaveChanges();
    }

    [HttpPut]
    public void Update(Chore updatedChore)
    {
        updatedChore = UpdateNextDueDate(updatedChore);
        _choreDB.Update(updatedChore);
        _choreDB.SaveChanges();
    }

    private Chore UpdateNextDueDate(Chore chore)
    {
        chore.NextDueDate = chore.CompletionDate.Add((TimeSpan)chore.Recurrence);
        return chore;
    }

    private Chore SetCompletionDateToToday(Chore chore, int dayOffset = 0)
    {
        chore.CompletionDate = DateTime.Today.AddDays(dayOffset);
        return chore;
    }
}

