using TheBillController.Application.Models;

namespace TheBillController.Application.Repositories;

public interface IExpenseRepository
{
    Task<bool> CreateAsync(Expense expense);

    Task<Expense?> GetAsync(Guid id);

    Task<IEnumerable<Expense>> GetMoreAsync(GetMoreExpensesOptions options);

    Task<bool> UpdateAsync(Expense expense);

    Task<bool> DeleteAsync(Guid id);

    Task<int> GetCountAsync(DateTime dateFrom, DateTime dateTo, Guid? typeId);
}
