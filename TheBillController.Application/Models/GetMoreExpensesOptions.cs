namespace TheBillController.Application.Models;

public class GetMoreExpensesOptions
{
    public required DateTime DateFrom { get; init; }

    public required DateTime DateTo { get; init; }

    public Guid? TypeId { get; set; }

    public string? SortBy { get; set; }

    public required SortOrder SortOrder { get; init; }

    public int Page { get;  set; }

    public int PageSize { get; set; }
}

public enum SortOrder
{
    Ascending,
    Descending
}
