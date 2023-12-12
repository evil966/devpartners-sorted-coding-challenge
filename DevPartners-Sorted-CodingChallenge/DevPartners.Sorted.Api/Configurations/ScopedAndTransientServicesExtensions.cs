using DevPartners.Sorted.Application.Services;

namespace DevPartners.Sorted.Api.Configurations;

public static class ScopedAndTransientServicesExtensions
{
    public static void AddScopedAndTransientServices(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddScoped<IRainfallServices, RainfallServices>();
    }
}
