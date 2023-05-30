using TheBillController.Api.Mapping;
using TheBillController.Application.Services;
using TheBillController.Contracts.Responses;

namespace TheBillController.Api.Endpoints.Expense;

public static class EndpointGetExpense
{
    public const string Name = "GetExpense";

    public static IEndpointRouteBuilder MapGetExpense(this IEndpointRouteBuilder app)
    {
        app.MapGet(
            ApiEndpoints.Expense.Get,
            async (Guid id, IExpenseService service) =>
            {
                var expense = await service.GetAsync(id);

                if (expense is null)
                {
                    return Results.NotFound();
                }

                var response = expense.MapToResponse();

                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<ExpenseResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
