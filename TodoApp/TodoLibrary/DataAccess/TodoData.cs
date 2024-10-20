using TodoLibrary.Models;
namespace TodoLibrary.DataAccess;

public class TodoData
{
    private readonly ISqlDataAccess _sql;
    public TodoData(ISqlDataAccess sql)
    {
        _sql = sql;
    }


    public Task<List<TodoModel>> GetAllAssigned(int assignedTo)
    {
        return _sql.LoadData<TodoModel, dynamic>(
            "spTodos_GetAllAssigned",
            new { AssignedTo = assignedTo },
            "Default");
    }


    /*
     * Use Async to Await the loading of the full List
     * Then Fetch Single Value which is Needed
     */

    public async Task<TodoModel?> GetOneAssigned(int assignedTo, int todoid)
    {
        var result = await _sql.LoadData<TodoModel, dynamic>(
            "spTodos_GetOneAssigned",
            new { AssignedTo = assignedTo, TodoID = todoid },
            "Default");

        return result.FirstOrDefault();
    }




}
