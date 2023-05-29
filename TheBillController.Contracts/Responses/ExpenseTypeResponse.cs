namespace TheBillController.Contracts.Responses;

public class ExpenseTypeResponse
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }
}
