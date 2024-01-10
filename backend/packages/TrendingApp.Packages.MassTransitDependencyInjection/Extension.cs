using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TrendingApp.Packages.MassTransitDependencyInjection;

public static class Extension
{
    public static IServiceCollection AddTrendingAppMassTransit(this IServiceCollection services)
        => services.AddMassTransit(ops =>
        {
            ops.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(false));

            var assembly = Assembly.GetEntryAssembly();

            ops.AddConsumers(assembly);

            ops.AddSagas(assembly);
            ops.AddSagaStateMachines(assembly);
            ops.SetInMemorySagaRepositoryProvider();

            ops.UsingRabbitMq((context, config) =>
            {
                config.Host(Constants.RabbitMqHost, h =>
                {
                    h.Username(Constants.RabbitMqUsername);
                    h.Password(Constants.RabbitMqPassword);
                });
                config.ConfigureEndpoints(context);
            });
        });
}
