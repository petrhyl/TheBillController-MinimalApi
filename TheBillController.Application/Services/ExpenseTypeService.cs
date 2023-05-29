using FluentValidation;
using TheBillController.Application.Models;
using TheBillController.Application.Repositories;

namespace TheBillController.Application.Services;

public class ExpenseTypeService : IExpenseTypeService
{
    private readonly IExpenseTypeRepository _expenseTypeRepository;
    private readonly IValidator<ExpenseType> _validator;

    public ExpenseTypeService(IExpenseTypeRepository expenseTypeRepository, IValidator<ExpenseType> validator)
    {
        _expenseTypeRepository = expenseTypeRepository;
        _validator = validator;
    }

    public async Task<bool> CreateAsync(ExpenseType expenseType)
    {
        await _validator.ValidateAndThrowAsync(expenseType);

        return await _expenseTypeRepository.CreateAsync(expenseType);
    }

    public async Task<IEnumerable<ExpenseType>> GetAllAsync()
    {
        return await _expenseTypeRepository.GetAllAsync();
    }

    public async Task<ExpenseType?> GetByIdAsync(Guid id)
    {
        return await _expenseTypeRepository.GetByIdAsync(id);
    }

    public async Task<ExpenseType?> GetByNameAsync(string name)
    {
        return await _expenseTypeRepository.GetByNameAsync(name);
    }

    public async Task<ExpenseType?> UpdateAsync(ExpenseType expenseType)
    {
        await _validator.ValidateAndThrowAsync(expenseType);

        var result = await _expenseTypeRepository.UpdateAsync(expenseType);

        if (!result)
        {
            return null;
        }

        return expenseType;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _expenseTypeRepository.DeleteAsync(id);
    }
}
