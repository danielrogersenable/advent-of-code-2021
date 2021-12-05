using System;
using AdventOfCode.Solutions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Solutions.StartupExtensions
{
    public static class AdventOfCodeSolutionsStartupExtensions
    {
        public static IServiceCollection AddAdventOfCodeSolutionsServices(this IServiceCollection services)
        {
            services.AddSingleton<DaySelectorService>();

            for (var i = 1; i <= 25; i++)
            {
                var dayName = string.Concat("0", i);
                var serviceTypeName = string.Concat("AdventOfCode.Solutions.Services.Day", dayName.Substring(dayName.Length - 2, 2));
                var serviceType = Type.GetType(serviceTypeName);

                if (serviceType != null)
                {
                    services.AddSingleton(serviceType);
                }
            }

            return services;
        }
    }
}
