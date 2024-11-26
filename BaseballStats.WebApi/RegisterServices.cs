using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Game.GetGamesFromSeries;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Infrastructure.Data.DataAccess;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BaseballStats.WebApi;

public static class RegisterServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.TryAddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddCommandHandlers();
        return services;
    }

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<GetGamesFromSeriesCommand, List<GameDto>>, GetGamesFromSeriesCommandHandler>();
        return services;
    }
}