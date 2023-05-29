using TheBillController.Application.Models;

namespace TheBillController.Application.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    public Task<bool> CreateAsync(Expense expense)
    {
        throw new NotImplementedException();
    }

    public Task<Expense?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Expense>> GetMoreAsync(GetMoreExpensesOptions options)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Expense expense)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCountAsync(DateTime dateFrom, DateTime dateTo, Guid? typeId)
    {
        throw new NotImplementedException();
    }
}
