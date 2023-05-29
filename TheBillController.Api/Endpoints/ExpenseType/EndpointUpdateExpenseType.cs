using TheBillController.Api.Mapping;
using TheBillController.Application.Services;
using TheBillController.Contracts.Requests;
using TheBillController.Contracts.Responses;

namespace TheBillController.Api.Endpoints.ExpenseType;

public static class EndpointUpdateExpenseType
{
    public const string Name = "UpdateExpenseType";

    public static IEndpointRouteBuilder MapUpdateExpenseType(this IEndpointRouteBuilder app)
    {
        app.MapPut(
            ApiEndpoints.ExpenseType.Update,
            async (Guid id, UpdateExpenseTypeRequest request, IExpenseTypeService service) =>
            {
                var expenseType = request.MapToExpenseType(id);
                var updatedExpenseType = await service.UpdateAsync(expenseType);

                if (updatedExpenseType is null)
                {
                    return Results.NotFound();
                }

                var response = updatedExpenseType.MapToResponse();

                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<ExpenseTypeResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest);

        return app;
    }
}
