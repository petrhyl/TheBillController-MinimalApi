using FluentValidation;
using TheBillController.Application.Models;
using TheBillController.Application.Repositories;

namespace TheBillController.Application.Validators;

public class ExpenseValidator:AbstractValidator<Expense>
{
    private readonly IExpenseTypeRepository _expenseTypeRepository;

    public ExpenseValidator(IExpenseTypeRepository expenseTypeRepository)
    {
        _expenseTypeRepository = expenseTypeRepository;

        RuleFor(x => x.Id)
            .NotEmpty();
        RuleFor(x => x.Description)
            .NotEmpty();
        RuleFor(x => x.DateOfExecution)
            .LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.Price)
            .GreaterThan(0);
        RuleFor(x => x.TypeId)
            .MustAsync(ValidateTypeId)
            .WithMessage("Assigned type of the expense does not exist.");
    }
    
    private async Task<bool> ValidateTypeId(Guid typeId, CancellationToken token = default)
    {
        return await FunctionsToValidate.ValidateTypeId(_expenseTypeRepository, typeId);
    }
}
