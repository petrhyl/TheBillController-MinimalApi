using TheBillController.Application.Models;

namespace TheBillController.Application.Repositories;

public interface IExpenseTypeRepository
{
    Task<bool> CreateAsync(ExpenseType expenseType);

    Task<ExpenseType?> GetByIdAsync(Guid id);

    Task<ExpenseType?> GetByNameAsync(string name);

    Task<IEnumerable<ExpenseType>> GetAllAsync();

    Task<bool> UpdateAsync(ExpenseType expenseType);

    Task<bool> DeleteAsync(Guid id);
}
