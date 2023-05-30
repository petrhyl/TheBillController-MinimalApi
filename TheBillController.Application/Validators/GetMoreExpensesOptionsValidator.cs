using FluentValidation;
using TheBillController.Application.Models;
using TheBillController.Application.Repositories;

namespace TheBillController.Application.Validators;

public class GetMoreExpensesOptionsValidator : AbstractValidator<GetMoreExpensesOptions>
{
    private readonly IExpenseTypeRepository _expenseTypeRepository;
    private readonly IExpenseRepository _expenseRepository;

    public GetMoreExpensesOptionsValidator(IExpenseTypeRepository expenseTypeRepository, IExpenseRepository expenseRepository)
    {
        _expenseTypeRepository = expenseTypeRepository;
        _expenseRepository = expenseRepository;

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
            .Must(x => x is null || AcceptableSortFields.SortFields.Contains(x, StringComparer.OrdinalIgnoreCase));
        RuleFor(x => x.Page)
            .MustAsync(ValidatePageNumber)
            .WithMessage("Cannot get the page number. You exceeded the number of records.");
    }

    private bool ValidateDateFrom(GetMoreExpensesOptions options, DateTime dateFrom)
    {
        return FunctionsToValidate.ValidateDateFrom(options);
    }

    private async Task<bool> ValidateTypeId(Guid? typeId, CancellationToken token = default)
    {
        if (typeId is null)
        {
            return true;
        }

        return await FunctionsToValidate.ValidateTypeId(_expenseTypeRepository, typeId.Value);
    }

    private async Task<bool> ValidatePageNumber(GetMoreExpensesOptions options, int page, CancellationToken token = default)
    {
        return await FunctionsToValidate.ValidatePageNumber(_expenseRepository, options);
    }
}
