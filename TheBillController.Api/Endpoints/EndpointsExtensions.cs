using TheBillController.Api.Endpoints.Expense;
using TheBillController.Api.Endpoints.ExpenseType;

namespace TheBillController.Api.Endpoints;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapExpenseTypeEndpoints();
        app.MapExpenseEndpoints();

        return app;
    }
}
