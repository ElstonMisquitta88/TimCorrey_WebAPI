using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoLibrary.DataAccess;
using TodoLibrary.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoData _data;
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodoData data, ILogger<TodosController> logger)
    {
        _data = data;
        _logger = logger;
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
        _logger.LogInformation("GET: {Api}", $"api/Todos");
        try
        {
            var output = await _data.GetAllAssigned(GetUserID());
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GET: {Api} Failed", $"api/Todos");
            return BadRequest();
        }
    }


    // GET api/Todos/5
    [HttpGet("{todoid}")]
    public async Task<ActionResult<TodoModel>> Get(int todoid)
    {
        _logger.LogInformation("GET: {Api}", $"api/Todos/{todoid}");
        try
        {
            var output = await _data.GetOneAssigned(GetUserID(), todoid);
            return Ok(output);
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "GET: {Api} Failed .The Id was {todoid}",
                $"api/Todos/Id"
                , todoid);
            return BadRequest();
        }
    }



    // POST api/Todos (Create a New TODO)
    [HttpPost]
    public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
    {
        _logger.LogInformation("POST: api/Todos Task Value was {task}", task);
        try
        {
            var output = await _data.Create(GetUserID(), task);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "POST: api/Todos Failed Task Value was {task}", task);
            return BadRequest();
        }
    }


    // PUT api/ /5 (Update a TODO Item)
    [HttpPut("{todoid}")]
    public async Task<ActionResult> Put(int todoid, [FromBody] string task)
    {
        _logger.LogInformation("PUT: api/  {task}", task);
        try
        {
            await _data.UpdateTask(GetUserID(), todoid, task);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PUT: api/ Failed {task}", task);
            return BadRequest();
        }
    }


    // PUT api/Todos/5/Complete
    [HttpPut("{todoid}/Complete")]
    public async Task<ActionResult> Complete(int todoid)
    {
        _logger.LogInformation("api/Todos/5/Complete");
        try
        {
            await _data.CompleteTodo(GetUserID(), todoid);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PUT: api/Todos/5/Complete Failed");
            return BadRequest();
        }
    }


    // DELETE api/Todos/5
    [HttpDelete("{todoid}")]
    public async Task<ActionResult> Delete(int todoid)
    {
        _logger.LogInformation("DELETE api/Todos/");
        try
        {
            await _data.Delete(GetUserID(), todoid);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DELETE: api/Todos/ Failed");
            return BadRequest();
        }
    }
}