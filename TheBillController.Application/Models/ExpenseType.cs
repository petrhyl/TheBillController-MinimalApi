namespace TheBillController.Application.Models;

public class ExpenseType
{
    public required Guid Id { get; init; }

    public required string Name { get; set; }

    public required string Description { get; set; }
}
