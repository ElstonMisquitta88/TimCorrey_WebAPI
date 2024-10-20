using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace TodoLibrary.DataAccess;
public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration Config)
    {
        _config = Config;
    }

    public async Task<List<T>> LoadData<T, U>(string storedprocedure, U parameters, string connectionStringName)
    {
        // (a) Fetch the Connection String
        string? ConnectionString = _config.GetConnectionString(connectionStringName);

        // (b) Create the Connection
        /*
         * This will live for this Method. Similar to using  -- using statement
         * This will close the connection at the end of the method
         */
        using IDbConnection connection = new SqlConnection(ConnectionString);

        //  (3) Return Data using Stored Procedure
        var rows = await connection.QueryAsync<T>(storedprocedure, parameters, commandType: CommandType.StoredProcedure);

        return rows.ToList();
    }

    public Task SaveData<T>(string storedprocedure, T parameters, string connectionStringName)
    {
        string? ConnectionString = _config.GetConnectionString(connectionStringName);
        using IDbConnection connection = new SqlConnection(ConnectionString);
        return connection.ExecuteAsync(storedprocedure, parameters, commandType: CommandType.StoredProcedure);
    }




}
