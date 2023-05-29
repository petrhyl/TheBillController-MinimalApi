namespace TheBillController.Api.Endpoints.Expense;

public static class ExpenseEndpointsExtensions
{
    public static IEndpointRouteBuilder MapExpenseEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateExpense();
        app.MapGetExpense();
        app.MapGetMoreExpenses();
        app.MapUpdateExpense();
        app.MapDeleteExpense();

        return app;
    }
}
