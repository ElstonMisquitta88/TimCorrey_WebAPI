using TodoLibrary.Models;

namespace TodoLibrary.DataAccess
{
    public interface ITodoData
    {
        Task CompleteTodo(int assignedTo, int todoid);
        Task<TodoModel?> Create(int assignedTo, string task);
        Task Delete(int assignedTo, int todoid);
        Task<List<TodoModel>> GetAllAssigned(int assignedTo);
        Task<TodoModel?> GetOneAssigned(int assignedTo, int todoid);
        Task UpdateTask(int assignedTo, int todoid, string task);
    }
}