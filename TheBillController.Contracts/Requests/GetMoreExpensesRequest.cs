namespace TheBillController.Contracts.Requests;

public class GetMoreExpensesRequest : PagedRequest
{
    public required DateTime DateFrom { get; init; }

    public required DateTime DateTo { get; init; }

    public Guid? TypeId { get; init; }

    public string? SortBy { get; init; }
}
