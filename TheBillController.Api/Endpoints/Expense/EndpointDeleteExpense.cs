using TheBillController.Application.Services;

namespace TheBillController.Api.Endpoints.Expense;

public static class EndpointDeleteExpense
{
    public const string Name = "DeleteExpense";

    public static IEndpointRouteBuilder MapDeleteExpense(this IEndpointRouteBuilder app)
    {
        app.MapDelete(
            ApiEndpoints.Expense.Delete,
            async (Guid id, IExpenseService service) =>
            {
                var deleted = await service.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.Ok();
            })
            .WithName(Name)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
