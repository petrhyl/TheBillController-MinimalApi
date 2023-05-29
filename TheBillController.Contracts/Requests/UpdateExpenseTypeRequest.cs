namespace TheBillController.Contracts.Requests;

public class UpdateExpenseTypeRequest
{
    public required string Name { get; init; }

    public required string Description { get; init; }
}
