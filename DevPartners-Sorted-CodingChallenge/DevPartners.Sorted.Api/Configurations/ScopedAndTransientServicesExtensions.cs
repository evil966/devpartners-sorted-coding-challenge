using DevPartners.Sorted.Api.Middleware;
using DevPartners.Sorted.Application.Services;

namespace DevPartners.Sorted.Api.Configurations;

public static class ScopedAndTransientServicesExtensions
{
    public static void AddScopedAndTransientServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IRainfallServices, RainfallServices>();
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }
}
