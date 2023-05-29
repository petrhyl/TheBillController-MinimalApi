using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TheBillController.Application.Database;
using TheBillController.Application.Repositories;
using TheBillController.Application.Services;

namespace TheBillController.Application;

public static class ApplicationServiceCollection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new SqliteConnectionFactory(connectionString));
        services.AddSingleton<DbInitializer>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IExpenseTypeRepository, ExpenseTypeRepository>();
        services.AddSingleton<IExpenseTypeService, ExpenseTypeService>();
        services.AddSingleton<IExpenseRepository, ExpenseRepository>();
        services.AddSingleton<IExpenseService, ExpenseService>();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);

        return services;
    }
}
