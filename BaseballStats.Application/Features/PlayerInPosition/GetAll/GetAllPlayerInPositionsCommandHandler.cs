using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInPosition.GetAll;

public class GetAllPlayerInPositionsCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetAllPlayerInPositionsCommand, List<PlayerInPositionCRUDDto>>
{
    public override Task<List<PlayerInPositionCRUDDto>> ExecuteAsync(GetAllPlayerInPositionsCommand command, CancellationToken ct = default)
    {
        var player_table = unitOfWork.Repository<Domain.Entities.Player>().DbSet;
        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;

        var result = 
            from pip in playerInPosition_table
            select new PlayerInPositionCRUDDto
            {
                PlayerId = pip.PlayerId,
                Position = pip.Position.GetDisplayName(),
                Effectiveness = pip.Effectiveness
            };

        return Task.FromResult(result.ToList());
    }
}