using FluentValidation;
using TheBillController.Application.Models;
using TheBillController.Application.Repositories;

namespace TheBillController.Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IValidator<Expense> _expenseValidator;
    private readonly IValidator<GetMoreExpensesOptions> _optionsValidator;
    private readonly IExpenseRepository _repository;

    public ExpenseService(IValidator<Expense> expenseValidator, IExpenseRepository repository, IValidator<GetMoreExpensesOptions> optionsValidator)
    {
        _expenseValidator = expenseValidator;
        _repository = repository;
        _optionsValidator = optionsValidator;
    }

    public async Task<bool> CreateAsync(Expense expense)
    {
        await _expenseValidator.ValidateAndThrowAsync(expense);

        return await _repository.CreateAsync(expense);
    }

    public async Task<Expense?> GetAsync(Guid id)
    {
        return await _repository.GetAsync(id);
    }

    public async Task<IEnumerable<Expense>> GetMoreAsync(GetMoreExpensesOptions options)
    {
        await _optionsValidator.ValidateAndThrowAsync(options);

        return await _repository.GetMoreAsync(options);
    }

    public async Task<bool> UpdateAsync(Expense expense)
    {
        await _expenseValidator.ValidateAndThrowAsync(expense);

        var existingExpense = await _repository.GetAsync(expense.Id);

        if (existingExpense is null)
        {
            return false;
        }

        return await _repository.UpdateAsync(expense);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<int> GetCountAsync(DateTime dateFrom, DateTime dateTo, Guid? typeId)
    {
        return await _repository.GetCountAsync(dateFrom, dateTo, typeId);
    }
}
