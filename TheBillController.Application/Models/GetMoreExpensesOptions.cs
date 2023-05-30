namespace TheBillController.Application.Models;

public class GetMoreExpensesOptions
{
    public required DateTime DateFrom { get; init; }

    public required DateTime DateTo { get; init; }

    public Guid? TypeId { get; set; }

    public string? SortBy { get; set; }

    public required SortOrder SortOrder { get; init; }

    public int Page { get; set; }

    public int PageSize { get; set; }
}

public static class AcceptableSortFields
{
    public const string Type = "Type";

    public const string DateOfExecution = "DateOfExecution";

    public const string Price = "Price";

    public static readonly string[] SortFields = {
        Type, DateOfExecution, Price
    };
}

public enum SortOrder
{
    Ascending,
    Descending
}
