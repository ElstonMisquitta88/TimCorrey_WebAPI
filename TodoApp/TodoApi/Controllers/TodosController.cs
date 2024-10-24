using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoLibrary.DataAccess;
using TodoLibrary.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoData _data;

    public TodosController(ITodoData data)
    {
        _data = data;
    }

    private int GetUserID()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId);
    }

    // GET: api/Todos  
    [HttpGet]
    public async Task<ActionResult<List<TodoModel>>> Get()
    {
        var output = await _data.GetAllAssigned(GetUserID());
        return Ok(output);
    }

    // GET api/Todos/5
    [HttpGet("{todoid}")]
    public async Task<ActionResult<TodoModel>> Get(int todoid)
    {
        var output = await _data.GetOneAssigned(GetUserID(), todoid);
        return Ok(output);
    }


    // POST api/Todos (Create a New TODO)
    [HttpPost]
    public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
    {
        var output = await _data.Create(GetUserID(), task);
        return Ok(output);
    }






    // PUT api/ /5 (Update a TODO Item)
    [HttpPut("{todoid}")]
    public async Task<ActionResult> Put(int todoid, [FromBody] string task)
    {
        await _data.UpdateTask(GetUserID(), todoid, task);
        return Ok();
    }







    // PUT api/Todos/5/Complete
    [HttpPut("{todoid}/Complete")]
    public async Task<ActionResult> Complete(int todoid)
    {
        await _data.CompleteTodo(GetUserID(), todoid);
        return Ok();
    }


    // DELETE api/Todos/5
    [HttpDelete("{todoid}")]
    public async Task<ActionResult> Delete(int todoid)
    {
        await _data.Delete(GetUserID(), todoid);
        return Ok();
    }
}