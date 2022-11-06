using Chores.Interfaces;
using Chores.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chores.Controllers;

[ApiController]
[Route("[controller]")]
public class ChoreController : ControllerBase
{
    private sqliteController choreDB;

    private readonly ILogger<ChoreController> _logger;

    public ChoreController(ILogger<ChoreController> logger)
    {
        _logger = logger;
        choreDB = new sqliteController();
    }

    [HttpGet]
    public List<Chore> Get()
    {
        return choreDB.GetAllChores();
    }

    [HttpDelete]
    public void Delete(int id)
    {
        choreDB.DeleteChore(id);
    }

    [HttpPost]
    public void Create(Chore newChore)
    {
        choreDB.AddChore(newChore);
    }

    [HttpPut]
    public void Update(Chore updatedChore)
    {
        choreDB.EditChore(updatedChore);
    }
}

