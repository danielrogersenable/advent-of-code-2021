using AdventOfCode.Solutions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Solutions.StartupExtensions
{
    public static class AdventOfCodeSolutionsStartupExtensions
    {
        public static IServiceCollection AddAdventOfCodeSolutionsServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<DaySelectorService>()
                .AddSingleton<Day01>()
                .AddSingleton<Day02>()
                .AddSingleton<Day03>()
                .AddSingleton<Day04>();
        }
    }
}
