using TheBillController.Application.Models;

namespace TheBillController.Application.Repositories;

public interface IExpenseRepository
{
    Task<bool> CreateAsync(Expense expense);

    Task<ExpenseContent?> GetAsync(Guid id);

    Task<IEnumerable<ExpenseContent>> GetMoreAsync(GetMoreExpensesOptions options);

    Task<bool> UpdateAsync(Expense expense);

    Task<bool> DeleteAsync(Guid id);

    Task<int> GetCountAsync(DateTime dateFrom, DateTime dateTo, Guid? typeId);
}
