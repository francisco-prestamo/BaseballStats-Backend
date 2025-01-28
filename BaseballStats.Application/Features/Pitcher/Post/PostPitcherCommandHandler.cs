using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Application.DTOs;
using FastEndpoints;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Pitcher.Post;

public class PostPitcherCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostPitcherCommand, PitcherDto>
{
    public override async Task<PitcherDto> ExecuteAsync(PostPitcherCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var pitcherRepository = unitOfWork.Repository<Domain.Entities.Pitcher>();
        var player_table = unitOfWork.Repository<Domain.Entities.Player>().DbSet;

        var player = (
            from p in player_table
            where p.Id == command.PlayerId
            select p
        ).First();

        var entity = new Domain.Entities.Pitcher()
        {
            Id = command.PlayerId,
            Player = player,
            GamesWonNumber = command.GamesWonNumber,
            GamesLostNumber = command.GamesLostNumber,
            RightHanded = command.RightHanded,
            AllowedRunsAvg = command.AllowedRunsAvg
        };

        var createdPitcher = await pitcherRepository.AddAsync(entity);
        
        var playerInPositionRepository = unitOfWork.Repository<Domain.Entities.PlayerInPosition>();

        var entityPip =
            (from p in player_table
             where p.Id == command.PlayerId
             select new Domain.Entities.PlayerInPosition()
             {
                 PlayerId = command.PlayerId,
                 Player = p,
                 Position = Domain.Enums.PlayerPositions.Pitcher,
                 Effectiveness = command.Effectiveness,
             }).ToList().First();

        await playerInPositionRepository.AddAsync(entityPip);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return createdPitcher.ToDto();
    }

    private async Task DatabaseValidations(PostPitcherCommand command)
    {
        var player_table = unitOfWork.Repository<Domain.Entities.Player>().DbSet;

        var player = (
            from p in player_table
            where p.Id == command.PlayerId
            select p
        ).ToList();

        if (player.IsNullOrEmpty())
            ThrowError("PlayerId not exists", StatusCodes.Status404NotFound);

        var pitcherRepo = unitOfWork.Repository<Domain.Entities.Pitcher>();
        var pitcher = await pitcherRepo.GetByIdAsync(command.PlayerId);

        if (pitcher is not null)
            ThrowError("PlayerId is already pitcher", StatusCodes.Status404NotFound);

        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;

        var entityPip = (
            from pip in playerInPosition_table
            where pip.PlayerId == command.PlayerId && pip.Position == Domain.Enums.PlayerPositions.Pitcher
            select pip
        ).ToList();

        if (!entityPip.IsNullOrEmpty())
            ThrowError("PlayerId is already pitcher", StatusCodes.Status404NotFound);

        await Task.CompletedTask;
    }
}