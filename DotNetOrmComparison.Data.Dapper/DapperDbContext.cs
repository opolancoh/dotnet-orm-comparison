using Npgsql;

namespace DotNetOrmComparison.Data.Dapper;

public class DapperDbContext
{
    private readonly string _connectionString;

    public DapperDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<NpgsqlConnection> CreateAndOpenConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}