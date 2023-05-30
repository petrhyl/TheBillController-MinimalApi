namespace TheBillController.Contracts.Responses;

public abstract class PagedResponse<TResponse>
{
    public IEnumerable<TResponse> Items { get; init; } = Enumerable.Empty<TResponse>();

    public int PageSize { get; init; }

    public int Page { get; init; }

    public int Total { get; init; }

    public bool HasNextPage => Total > Page * PageSize;
}
