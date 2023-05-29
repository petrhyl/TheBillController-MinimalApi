using FluentValidation;
using TheBillController.Application.Models;
using TheBillController.Application.Repositories;

namespace TheBillController.Application.Validators;

public class GetMoreExpensesOptionsValidator : AbstractValidator<GetMoreExpensesOptions>
{
    private readonly IExpenseTypeRepository _expenseTypeRepository;
    private static readonly string[] AcceptableSortFields =
    {
        "type", "dateOfExecution", "price"
    };

    public GetMoreExpensesOptionsValidator(IExpenseTypeRepository expenseTypeRepository)
    {
        _expenseTypeRepository = expenseTypeRepository;

        RuleFor(x => x.DateFrom)
            .NotEmpty()
            .Must(ValidateDateFrom)
            .WithMessage("Date from has to be less or equal today's date and date to.");
        RuleFor(x => x.DateTo)
            .NotEmpty();
        RuleFor(x => x.TypeId)
            .MustAsync(ValidateTypeId)
            .WithMessage("Assigned type of the expense does not exist."); 
        RuleFor(x => x.SortBy)
            .Must(x => x is null || AcceptableSortFields.Contains(x, StringComparer.OrdinalIgnoreCase));
    }

    private bool ValidateDateFrom(GetMoreExpensesOptions options, DateTime dateFrom)
    {
        return FunctionsToValidate.ValidateDateFrom(options, dateFrom);
    }

    private async Task<bool> ValidateTypeId(Guid? typeId, CancellationToken token = default)
    {
        if (typeId is null)
        {
            return true;
        }

        return await FunctionsToValidate.ValidateTypeId(_expenseTypeRepository, typeId.Value!);
    }
}
