namespace TheBillController.Contracts.Responses;

public class ExpensesResponse : IPagedResponse<ExpenseResponse>
{
    public IEnumerable<ExpenseResponse> Items { get; init; } = Enumerable.Empty<ExpenseResponse>();

    public int PageSize { get; init; }

    public int Page { get; init; }

    public int Total { get; init; }

    public bool HasNextPage => Total > Page * PageSize;
}
