using TheBillController.Application.Models;

namespace TheBillController.Application.Services;

public interface IExpenseTypeService
{
    Task<bool> CreateAsync(ExpenseType expenseType);

    Task<ExpenseType?> GetByIdAsync(Guid id);

    Task<ExpenseType?> GetByNameAsync(string name);

    Task<IEnumerable<ExpenseType>> GetAllAsync();

    Task<ExpenseType?> UpdateAsync(ExpenseType expenseType);

    Task<bool> DeleteAsync(Guid id);   
}
