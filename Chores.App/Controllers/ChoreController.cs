using Chores.Interfaces;
using Chores.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chores.Controllers;

[ApiController]
[Route("[controller]")]
public class ChoreController : ControllerBase
{
    private IDBInterface _choreDB;

    private readonly ILogger<ChoreController> _logger;

    public ChoreController(ILogger<ChoreController> logger, IDBInterface choreDB)
    {
        _logger = logger;
        _choreDB = choreDB;
    }

    [HttpGet]
    public List<Chore> Get()
    {
        return _choreDB.GetAllChores();
    }

    [HttpGet("{id}")]
    public Chore GetById(int id)
    {
        return _choreDB.GetChore(id);
    }

    [HttpDelete]
    public void Delete(int id)
    {
        _choreDB.DeleteChore(id);
    }

    [HttpPost]
    public void Add(Chore newChore)
    {
        newChore = UpdateNextDueDate(newChore);
        _choreDB.AddChore(newChore);
    }

    [HttpPut]
    public void Update(Chore updatedChore)
    {
        updatedChore = UpdateNextDueDate(updatedChore);
        _choreDB.EditChore(updatedChore);
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

