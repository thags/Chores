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

    [HttpDelete]
    public void Delete(int id)
    {
        _choreDB.DeleteChore(id);
    }

    [HttpPost]
    public void Create(Chore newChore)
    {
        _choreDB.AddChore(newChore);
    }

    [HttpPut]
    public void Update(Chore updatedChore)
    {
        _choreDB.EditChore(updatedChore);
    }
}

