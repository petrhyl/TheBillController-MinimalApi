using TheBillController.Api.Mapping;
using TheBillController.Application.Services;
using TheBillController.Contracts.Requests;
using TheBillController.Contracts.Responses;

namespace TheBillController.Api.Endpoints.Expense;

public static class EndpointCreateExpense
{
    public const string Name = "CreateExpense";

    public static IEndpointRouteBuilder MapCreateExpense(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            ApiEndpoints.Expense.Create,
            async (CreateExpenseRequest request, IExpenseService service) =>
            {
                var expense = request.MapToExpense();
                var expenseContent = await service.CreateAsync(expense);
                var response = expenseContent.MapToResponse();

                return TypedResults.CreatedAtRoute(response, EndpointGetExpense.Name, new {id = expense.Id});
            })
            .WithName(Name)
            .Produces<ExpenseResponse>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest); 

        return app;
    }
}
