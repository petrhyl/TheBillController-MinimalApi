using TheBillController.Api.Mapping;
using TheBillController.Application.Services;
using TheBillController.Contracts.Responses;

namespace TheBillController.Api.Endpoints.ExpenseType;

public static class EndpointGetExpenseType
{
    public const string Name = "GetExpenseType";

    public static IEndpointRouteBuilder MapGetExpenseType(this IEndpointRouteBuilder app)
    {
        app.MapGet(
            ApiEndpoints.ExpenseType.Get,
            async (string idOrName, IExpenseTypeService service) =>
            {
                var expenseType = Guid.TryParse(idOrName, out var typeId)
                    ? await service.GetByIdAsync(typeId)
                    : await service.GetByNameAsync(idOrName);

                if (expenseType is null)
                {
                    return Results.NotFound();
                }

                var response = expenseType.MapToResponse();

                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<ExpenseTypeResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
