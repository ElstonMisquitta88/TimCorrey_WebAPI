using TodoLibrary.Models;
namespace TodoLibrary.DataAccess;

public class TodoData : ITodoData
{
    private readonly ISqlDataAccess _sql;
    public TodoData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    //Use Async to Await the loading of the full List.Then Fetch Single Value which is Needed


    public Task<List<TodoModel>> GetAllAssigned(int assignedTo)
    {
        return _sql.LoadData<TodoModel, dynamic>(
            "spTodos_GetAllAssigned",
            new { AssignedTo = assignedTo },
            "Default");
    }
    public async Task<TodoModel?> GetOneAssigned(int assignedTo, int todoid)
    {
        var result = await _sql.LoadData<TodoModel, dynamic>(
            "spTodos_GetOneAssigned",
            new { AssignedTo = assignedTo, TodoID = todoid },
            "Default");

        return result.FirstOrDefault();
    }
    public async Task<TodoModel?> Create(int assignedTo, string task)
    {
        var result = await _sql.LoadData<TodoModel, dynamic>(
            "spTodos_Create",
            new { AssignedTo = assignedTo, Task = task },
            "Default");

        return result.FirstOrDefault();
    }



    public Task UpdateTask(int assignedTo, int todoid, string task)
    {
        return _sql.SaveData<dynamic>(
            "spTodos_UpdateTask",
            new { Task = task, AssignedTo = assignedTo, TodoID = todoid },
            "Default");
    }
    public Task CompleteTodo(int assignedTo, int todoid)
    {
        return _sql.SaveData<dynamic>(
            "spTodos_CompleteTodo",
            new { AssignedTo = assignedTo, TodoID = todoid },
            "Default");
    }
    public Task Delete(int assignedTo, int todoid)
    {
        return _sql.SaveData<dynamic>(
            "spTodos_DeleteTodo",
            new { AssignedTo = assignedTo, TodoID = todoid },
            "Default");
    }

}
