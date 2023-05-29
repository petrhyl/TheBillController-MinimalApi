namespace TheBillController.Api.Endpoints.ExpenseType;

public static class ExpenseTypeEndpointsExtensions
{
    public static IEndpointRouteBuilder MapExpenseTypeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateExpenseType();
        app.MapGetExpenseType();
        app.MapGetAllExpenseTypes();
        app.MapUpdateExpenseType();
        app.MapDeleteExpenseType();

        return app;
    }
}
