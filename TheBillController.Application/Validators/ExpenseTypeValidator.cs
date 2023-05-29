using FluentValidation;
using TheBillController.Application.Models;
using TheBillController.Application.Repositories;

namespace TheBillController.Application.Validators;

public class ExpenseTypeValidator: AbstractValidator<ExpenseType>
{
    private readonly IExpenseTypeRepository _expenseTypeRepository;

    public ExpenseTypeValidator(IExpenseTypeRepository expenseTypeRepository)
    {
        _expenseTypeRepository = expenseTypeRepository;

        RuleFor(x => x.Id)
            .NotEmpty();
        RuleFor(x => x.Name)
            .NotEmpty()
            .MustAsync(ValidateName)
            .WithMessage("This expense type already exists in the system.");
        RuleFor(x => x.Description)
            .NotEmpty();
    }

    private async Task<bool> ValidateName(ExpenseType expenseType, string name, CancellationToken cancellationToken = default)
    {
        return await FunctionsToValidate.ValidateName(_expenseTypeRepository, expenseType, name);
    }
}
