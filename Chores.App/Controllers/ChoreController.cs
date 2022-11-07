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

    [HttpGet("GetById/{id}")]
    public Chore Get(int id)
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
        newChore.NextDueDate = UpdateNextDueDate(newChore);
        _choreDB.AddChore(newChore);
    }

    [HttpPut]
    public void Update(Chore updatedChore)
    {
        updatedChore.NextDueDate = UpdateNextDueDate(updatedChore);
        _choreDB.EditChore(updatedChore);
    }

    [HttpPut]
    public void MarkCompleted(Chore completedChore)
    {
        completedChore.CompletionDate = DateTime.Today;
        completedChore.NextDueDate = UpdateNextDueDate(completedChore);
        _choreDB.EditChore(completedChore);
    }

    private DateTime UpdateNextDueDate(Chore chore)
    {
        return DateTime.Now.Add((TimeSpan)chore.Recurrence);
    }
}

