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
}
