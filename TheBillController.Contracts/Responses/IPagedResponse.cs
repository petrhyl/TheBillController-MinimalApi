namespace TheBillController.Contracts.Responses;

public interface IPagedResponse<TResponse>
{
    public IEnumerable<TResponse> Items { get; init; }

    public int PageSize { get; init; }

    public int Page { get; init; }

    public int Total { get; init; }

    public bool HasNextPage { get; }
}
