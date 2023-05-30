using TheBillController.Api.Mapping;
using TheBillController.Application.Services;
using TheBillController.Contracts.Requests;
using TheBillController.Contracts.Responses;

namespace TheBillController.Api.Endpoints.Expense;

public static class EndpointGetMoreExpenses
{
    public const string Name = "GetMoreExpenses";

    public static IEndpointRouteBuilder MapGetMoreExpenses(this IEndpointRouteBuilder app)
    {
        app.MapGet(
            ApiEndpoints.Expense.GetMore,
            async ([AsParameters] GetMoreExpensesRequest request, IExpenseService service) =>
            {
                var options = request.MapToOptions();
                var expenses = await service.GetMoreAsync(options);
                var expensesAmount = await service.GetCountAsync(options.DateFrom, options.DateTo, options.TypeId);
                var response = expenses.MapToResponse(options.Page, options.PageSize, expensesAmount);

                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<ExpenseResponse>(StatusCodes.Status200OK)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest);

        return app;
    }
}
