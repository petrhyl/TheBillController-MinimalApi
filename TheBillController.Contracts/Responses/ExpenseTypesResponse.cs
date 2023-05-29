namespace TheBillController.Contracts.Responses;

public class ExpenseTypesResponse
{
    public required IEnumerable<ExpenseTypeResponse> Items { get; init; }
}
