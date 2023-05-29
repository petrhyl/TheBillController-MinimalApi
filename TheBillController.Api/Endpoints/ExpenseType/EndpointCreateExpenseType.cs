using TheBillController.Api.Mapping;
using TheBillController.Application.Services;
using TheBillController.Contracts.Requests;
using TheBillController.Contracts.Responses;

namespace TheBillController.Api.Endpoints.ExpenseType;

public static class EndpointCreateExpenseType
{
    public const string Name = "CreateExpenseType";

    public static IEndpointRouteBuilder MapCreateExpenseType(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            ApiEndpoints.ExpenseType.Create,
            async (CreateExpenseTypeRequest request, IExpenseTypeService service) =>
            {
                var expenseType = request.MapToExpenseType();
                await service.CreateAsync(expenseType);
                var response = expenseType.MapToResponse();

                return TypedResults.CreatedAtRoute(response, EndpointGetExpenseType.Name, new { idOrName = expenseType.Id });
            })
            .WithName(Name)
            .Produces<ExpenseTypeResponse>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest);

        return app;
    }
}
