using TheBillController.Api.Mapping;
using TheBillController.Application.Services;
using TheBillController.Contracts.Requests;
using TheBillController.Contracts.Responses;

namespace TheBillController.Api.Endpoints.Expense;

public static class EndpointUpdateExpense
{
    public const string Name = "UpdateExpense";

    public static IEndpointRouteBuilder MapUpdateExpense(this IEndpointRouteBuilder app)
    {
        app.MapPut(
            ApiEndpoints.Expense.Update,
            async (Guid id, UpdateExpenseRequest request, IExpenseService service) =>
            {
                var expense = request.MapToExpense(id);
                var updatedExpense = await service.UpdateAsync(expense);

                if (updatedExpense is null)
                {
                    return Results.NotFound();
                }

                var respose = updatedExpense.MapToResponse();

                return TypedResults.Ok(respose);
            })
            .WithName(Name)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest);

        return app;
    }
}
