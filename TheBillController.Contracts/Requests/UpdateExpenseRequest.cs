namespace TheBillController.Contracts.Requests;

public class UpdateExpenseRequest
{
    public required string Description { get; init; }

    public required DateTime DateOfExecution { get; init; }

    public required double Price { get; init; }

    public required Guid TypeId { get; init; }
}
