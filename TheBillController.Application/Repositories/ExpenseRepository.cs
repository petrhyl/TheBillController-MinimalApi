using Dapper;
using TheBillController.Application.Database;
using TheBillController.Application.Models;

namespace TheBillController.Application.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ExpenseRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(Expense expense)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO Expenses (Id, Description, DateOfExecution, Price, TypeId)
            VALUES (@Id, @Description, @DateOfExecution, @Price, @TypeId)", expense);

        return result > 0;
    }

    public async Task<ExpenseContent?> GetAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QuerySingleOrDefaultAsync<ExpenseContent>(
            @"SELECT e.*, t.Name as TypeName FROM Expenses e 
            LEFT JOIN ExpenseTypes t ON e.TypeId = t.Id
            WHERE e.Id = @Id LIMIT 1", new { Id = id }
        );
    }

    public async Task<IEnumerable<ExpenseContent>> GetMoreAsync(GetMoreExpensesOptions options)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var filterClause = string.Empty;
        if (options.TypeId is not null)
        {
            filterClause = $"AND t.Id = {options.TypeId}";
        }

        var orderByDate = "DESC";
        var sortOrder = options.SortOrder == SortOrder.Descending ? "DESC" : "ASC";
        var orderClause = string.Empty;
        if (options.SortBy is not null)
        {
            if (options.SortBy.ToLower() != AcceptableSortFields.DateOfExecution.ToLower())
            {
                orderClause = $", e.{options.SortBy} {sortOrder}";
            }
            else
            {
                orderByDate = sortOrder;
            }
        }

        orderClause = $"ORDER BY e.DateOfExecution {orderByDate}{orderClause}";

        return await connection.QueryAsync<ExpenseContent>($"""
              SELECT e.*, t.Name as TypeName FROM Expenses e
              LEFT JOIN ExpenseTypes t ON e.TypeId = t.Id
              WHERE e.DateOfExecution >= @DateFrom AND e.DateOfExecution <= @DateTo {filterClause}
              {orderClause}
              LIMIT @PageSize OFFSET @PageOffset
              """,
              new {
                  DateFrom = options.DateFrom,
                  DateTo = options.DateTo,
                  PageSize = options.PageSize,
                  PageOffset = (options.Page - 1) * options.PageSize
              });
    }

    public async Task<bool> UpdateAsync(Expense expense)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"UPDATE Expenses 
            SET Description = @Description, DateOfExecution = @DateOfExecution, Price = @Price, TypeId = @TypeId
            WHERE Id = @Id",
            expense);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            "DELETE FROM Expenses WHERE Id = @Id", new { Id = id });

        return result > 0;
    }

    public async Task<int> GetCountAsync(DateTime dateFrom, DateTime dateTo, Guid? typeId)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var typeCondition = string.Empty;
        if (typeId is not null)
        {
            typeCondition = $"AND TypeId = {typeId}";
        }

        return await connection.QuerySingleAsync<int>($"""
            SELECT COUNT(Id) FROM Expenses
            WHERE DateOfExecution >= @dateFrom AND DateOfExecution <= @dateTo {typeCondition}
            """, new { dateFrom, dateTo });
    }
}
