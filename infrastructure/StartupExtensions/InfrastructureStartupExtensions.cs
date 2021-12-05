using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.StartupExtensions
{
    public static class InfrastructureStartupExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<InputParserService>()
                .AddSingleton<BinaryService>();
        }
    }
}
