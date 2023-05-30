using TheBillController.Application.Models;
using TheBillController.Contracts.Requests;
using TheBillController.Contracts.Responses;

namespace TheBillController.Api.Mapping;

public static class ContractMapping
{
    public static ExpenseType MapToExpenseType(this CreateExpenseTypeRequest request)
    {
        return new ExpenseType {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };
    }

    public static ExpenseType MapToExpenseType(this UpdateExpenseTypeRequest request, Guid id)
    {
        return new ExpenseType {
            Id = id,
            Name = request.Name,
            Description = request.Description
        };
    }

    public static ExpenseTypeResponse MapToResponse(this ExpenseType expenseType)
    {
        return new ExpenseTypeResponse {
            Id = expenseType.Id,
            Name = expenseType.Name,
            Description = expenseType.Description,
        };
    }

    public static ExpenseTypesResponse MapToResponse(this IEnumerable<ExpenseType> expenseTypes)
    {
        return new ExpenseTypesResponse {
            Items = expenseTypes.Select(MapToResponse)
        };
    }

    public static Expense MapToExpense(this CreateExpenseRequest request)
    {
        return new Expense {
            Id = Guid.NewGuid(),
            Description = request.Description,
            DateOfExecution = request.DateOfExecution,
            Price = request.Price,
            TypeId = request.TypeId
        };
    }

    public static ExpenseResponse MapToResponse(this Expense expense)
    {
        return new ExpenseResponse {
            Id = expense.Id,
            Description = expense.Description,
            DateOfExecution = expense.DateOfExecution,
            Price = expense.Price,
            TypeId = expense.TypeId
        };
    }

    public static ExpensesResponse MapToResponse(this IEnumerable<Expense> expenses, int page, int pageSize, int totalCount)
    {
        return new ExpensesResponse {
            Items = expenses.Select(MapToResponse),
            Page = page,
            PageSize = pageSize,
            Total = totalCount
        };
    }

    public static GetMoreExpensesOptions MapToOptions(this GetMoreExpensesRequest request)
    {
        return new GetMoreExpensesOptions {
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
            TypeId = request.TypeId,
            SortOrder = (request.SortBy != null && request.SortBy.StartsWith("-")) ? SortOrder.Ascending : SortOrder.Descending,
            SortBy = request.SortBy?.Trim('+', '-'),
            Page = request.Page.GetValueOrDefault(PagedRequest.DefaultPage),
            PageSize = request.PageSize.GetValueOrDefault(PagedRequest.DefaultPageSize)
        };
    }

    public static Expense MapToExpense(this UpdateExpenseRequest request, Guid id)
    {
        return new Expense {
            Id = id,
            Description = request.Description,
            DateOfExecution = request.DateOfExecution,
            Price = request.Price,
            TypeId = request.TypeId
        };
    }
}
