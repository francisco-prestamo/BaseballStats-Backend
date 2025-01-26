using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.Player.GetAll;

public class GetAllPlayersCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetAllPlayersCommand, List<RegularPlayerDto>>
{
    public override async Task<List<RegularPlayerDto>> ExecuteAsync(GetAllPlayersCommand command, CancellationToken cancellationToken = default)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();

        var entities = await playerRepository.GetAllAsync();

        return entities.Select(x => x.ToDto()).ToList();
    }
}