namespace TheBillController.Application.Models;

public class Expense
{
    public required Guid Id { get; init; }

    public required string Description { get; set; }

    public required DateTime DateOfExecution { get; set; }

    public required double Price { get; set; }

    public required Guid TypeId { get; set; }
}
