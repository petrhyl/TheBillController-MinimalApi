using TheBillController.Application.Models;
using TheBillController.Application.Repositories;

namespace TheBillController.Application.Validators;

internal static class FunctionsToValidate
{
    internal static async Task<bool> ValidateName(IExpenseTypeRepository expenseTypeRepository, ExpenseType expenseType, string name)
    {
        var existingType = await expenseTypeRepository.GetByNameAsync(name);

        if (existingType is not null)
        {
            return existingType.Id == expenseType.Id;
        }

        return existingType is null;
    }

    internal static async Task<bool> ValidateTypeId(IExpenseTypeRepository expenseTypeRepository, Guid typeId)
    {
        var result = await expenseTypeRepository.GetByIdAsync(typeId);

        if (result is null)
        {
            return false;
        }

        return true;
    }

    internal static bool ValidateDateFrom(GetMoreExpensesOptions options)
    {
        if (options.DateFrom > DateTime.UtcNow || options.DateFrom > options.DateTo)
        {
            return false;
        }

        return true;
    }

    internal static async Task<bool> ValidatePageNumber(IExpenseRepository expenseRepository, GetMoreExpensesOptions options)
    {
        var recordCount = await expenseRepository.GetCountAsync(options.DateFrom, options.DateTo, options.TypeId);

        if (recordCount <= ((options.Page - 1) * options.PageSize))
        {
            return false;
        }

        return true;
    }
}
