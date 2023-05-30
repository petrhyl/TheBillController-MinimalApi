using TheBillController.Application.Models;

namespace TheBillController.Application.Services;

public interface IExpenseService
{
    Task<ExpenseContent> CreateAsync(Expense expense);

    Task<ExpenseContent?> GetAsync(Guid id);

    Task<IEnumerable<ExpenseContent>> GetMoreAsync(GetMoreExpensesOptions options);

    Task<ExpenseContent?> UpdateAsync(Expense expense);

    Task<bool> DeleteAsync(Guid id);

    Task<int> GetCountAsync(DateTime dateFrom, DateTime dateTo, Guid? typeId);
}
