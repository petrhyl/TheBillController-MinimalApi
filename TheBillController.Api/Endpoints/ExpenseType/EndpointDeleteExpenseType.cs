using TheBillController.Application.Services;

namespace TheBillController.Api.Endpoints.ExpenseType;

public static class EndpointDeleteExpenseType
{
    public const string Name = "DeleteExpenseType";

    public static IEndpointRouteBuilder MapDeleteExpenseType(this IEndpointRouteBuilder app)
    {
        app.MapDelete(
            ApiEndpoints.ExpenseType.Delete,
            async (Guid id, IExpenseTypeService service) =>
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
