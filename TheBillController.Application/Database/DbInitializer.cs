using Dapper;

namespace TheBillController.Application.Database;

public class DbInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DbInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        await connection.ExecuteAsync(
            @"CREATE TABLE IF NOT EXISTS ExpenseTypes (
                Id UUID PRIMARY KEY, 
                Name TEXT NOT NULL,
                Description TEXT NOT NULL
            )"
        );

        await connection.ExecuteAsync(
            @"CREATE TABLE IF NOT EXISTS Expenses (
                Id UUID PRIMARY KEY, 
                Description TEXT NOT NULL,
                DateOfExecution DATETIME NOT NULL,
                Price REAL NOT NULL,
                TypeId UUID NOT NULL,
                FOREIGN KEY(TypeId) REFERENCES ExpenseTypes(Id)
            )"
        );
    }
}
