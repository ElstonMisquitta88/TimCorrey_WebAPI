
namespace TodoLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string storedprocedure, U parameters, string connectionStringName);
        Task SaveData<T>(string storedprocedure, T parameters, string connectionStringName);
    }
}