namespace TheBillController.Contracts.Requests;

public class CreateExpenseTypeRequest
{
    public required string Name { get; init; }

    public required string Description { get; init; }
}
