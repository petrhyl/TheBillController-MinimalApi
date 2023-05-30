namespace TheBillController.Contracts.Responses;

public class ExpenseResponse
{
    public required Guid Id { get; init; }

    public required string Description { get; init; }

    public required DateTime DateOfExecution { get; init; }

    public required double Price { get; init; }

    public required Guid TypeId { get; init; }

    public required string TypeName { get; init; }
}
