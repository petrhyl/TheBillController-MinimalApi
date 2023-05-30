using Dapper;
using TheBillController.Application.Database;
using TheBillController.Application.Models;

namespace TheBillController.Application.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private const string DateOrder = "DateOfExecution";

    public ExpenseRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(Expense expense)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO Expenses (Id, Description, DateOfExecution, Price, TypeId)
            VALUES (@Id, @Description, @DateOfExecution, @Price, @TypeId", expense);

        return result > 0;
    }

    public async Task<Expense?> GetAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QuerySingleOrDefaultAsync<Expense>(
            "SELECT * FROM Expenses WHERE Id = @Id LIMIT 1", new { Id = id }
        );
    }

    public async Task<IEnumerable<Expense>> GetMoreAsync(GetMoreExpensesOptions options)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var filterClause = string.Empty;
        if (options.TypeId is not null)
        {
            filterClause = $"AND t.Id = {options.TypeId}";
        }

        var orderClause = string.Empty;
        var orderTrend = options.SortOrder == SortOrder.Descending ? "DESC" : "ASC";
        if (options.SortBy is not null)
        {
            if (options.SortBy.ToLower() != DateOrder.ToLower())
            {
                orderClause = $"e.{options.SortBy} {orderTrend}";
            }
        }

        orderClause = $"ORDER BY e.DateOfExecution {orderTrend}, {orderClause} ";

        var result = await connection.QueryAsync<Expense>($"""
              SELECT e.*, t.Name FROM Expenses e
              LEFT JOIN ExpenseTypes t ON e.TypeId = t.Id
              WHERE e.DateOfExecution >= @DateFrom AND e.DateOfExecution <= @DateTo
              """);
    }

    public Task<bool> UpdateAsync(Expense expense)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCountAsync(DateTime dateFrom, DateTime dateTo, Guid? typeId)
    {
        throw new NotImplementedException();
    }
}
