using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.GetAll;

public class GetAllGamesCommandHadler(IUnitOfWork unitOfWork) : CommandHandler<GetAllGamesCommand, List<GameDto>>
{
    public override async Task<List<GameDto>> ExecuteAsync(GetAllGamesCommand command, CancellationToken ct = default)
    {
        var games = (await unitOfWork.Repository<Domain.Entities.Game>().GetAllAsync()).ToList();

        return games.Select(g => g.ToGameDto()).ToList();
    } 

}