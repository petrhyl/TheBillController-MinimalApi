using Dapper;
using System.Data;
using TheBillController.Application.Database;
using TheBillController.Application.Models;

namespace TheBillController.Application.Repositories;

public class ExpenseTypeRepository : IExpenseTypeRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ExpenseTypeRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(ExpenseType expenseType)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO ExpenseTypes (Id, Name, Description) 
            VALUES (@Id, @Name, @Description)",
            expenseType);

        return result > 0;
    }

    public async Task<IEnumerable<ExpenseType>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryAsync<ExpenseType>("SELECT * FROM ExpenseTypes");
    }

    public async Task<ExpenseType?> GetByIdAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QuerySingleOrDefaultAsync<ExpenseType>(
            "SELECT * FROM ExpenseTypes WHERE Id = @Id LIMIT 1", new { Id = id }
        );
    }

    public async Task<ExpenseType?> GetByNameAsync(string name)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QuerySingleOrDefaultAsync<ExpenseType>(
            "SELECT * FROM ExpenseTypes WHERE Name = @Name LIMIT 1", new { Name = name }
        );
    }

    public async Task<bool> UpdateAsync(ExpenseType expenseType)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"UPDATE ExpenseTypes SET Name = @Name, Description = @Description
            WHERE Id = @Id",
            expenseType);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@"DELETE FROM ExpenseTypes WHERE Id = @Id", new { Id = id });

        return result > 0;
    }
}
