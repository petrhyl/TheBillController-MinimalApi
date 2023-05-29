using TheBillController.Api.Mapping;
using TheBillController.Application.Services;
using TheBillController.Contracts.Responses;

namespace TheBillController.Api.Endpoints.ExpenseType;

public static class EndpointGetAllExpenseTypes
{
    public const string Name = "GetAllExpenseTypes";

    public static IEndpointRouteBuilder MapGetAllExpenseTypes(this IEndpointRouteBuilder app)
    { 
        app.MapGet(
            ApiEndpoints.ExpenseType.GetAll,
            async (IExpenseTypeService service) =>
            {
                var expenseTypes = await service.GetAllAsync();
                var response = expenseTypes.MapToResponse();

                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<ExpenseTypesResponse>(StatusCodes.Status200OK);

        return app;
    }
}
