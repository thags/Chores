using Chores.Interfaces;
using Chores.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chores.Controllers;

[ApiController]
[Route("[controller]")]
public class ChoreController : ControllerBase
{
    private IChoreDB _choreRepo;

    private readonly ILogger<ChoreController> _logger;

    public ChoreController(ILogger<ChoreController> logger, IChoreDB repo)
    {
        _logger = logger;
        _choreRepo = repo;
    }

    [HttpGet]
    public IEnumerable<Chore> Get()
    {
        return _choreRepo.Get();
    }

    [HttpGet("{id}")]
    public Chore GetById(int id)
    {
        return _choreRepo.GetById(id);
    }

    [HttpDelete]
    public void Delete(Chore choreToRemove)
    {
        _choreRepo.Delete(choreToRemove);
    }

    [HttpDelete]
    public void DeleteById(int id)
    {
        _choreRepo.DeleteById(id);
    }

    [HttpPost]
    public void Add(Chore newChore)
    {
        _choreRepo.Add(newChore);
    }

    [HttpPut]
    public void Update(Chore updatedChore)
    {
        _choreRepo.Update(updatedChore);
    }
}

