using System;
using AdventOfCode.Solutions.Services;
using AdventOfCode.Solutions.StartupExtensions;
using Infrastructure.StartupExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services
                    .AddAdventOfCodeSolutionsServices()
                    .AddInfrastructureServices();
            })
            .Build();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var daySelectorService = serviceProvider.GetRequiredService<DaySelectorService>();

            daySelectorService.SelectDay();
        }
    }
}
