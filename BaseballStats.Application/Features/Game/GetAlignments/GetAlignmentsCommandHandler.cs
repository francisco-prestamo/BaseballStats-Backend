using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BaseballStats.Application.Features.Game.GetAlignments;

public class GetAlignmentsCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetAlignmentsCommand, AlignmentsDto>
{
    public override async Task<AlignmentsDto> ExecuteAsync(GetAlignmentsCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var gameId = command.GameId;

        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();

        var games = gameRepository.Where(x => x.Id == gameId).ToList();
        var team1Id = games.First().Team1Id;
        var team2Id = games.First().Team2Id;
        
        var alignmentsRepository = unitOfWork.Repository<Domain.Entities.AlignedPlayerInGame>();
    
        var playersTeam1 = alignmentsRepository.Where(x => x.GameId == gameId && x.TeamId == team1Id).ToList();
        var playersTeam2 = alignmentsRepository.Where(x => x.GameId == gameId && x.TeamId == team2Id).ToList();

        var alignmentsDto = (playersTeam1, playersTeam2).ToDto();

        return alignmentsDto;
    }

    private async Task DatabaseValidations(GetAlignmentsCommand command)
    {
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var game = await gameRepository.GetByIdAsync(command.GameId);

        if (game is null)
            ThrowError("GameId not found", StatusCodes.Status404NotFound);
        
        var gameId = command.GameId;

        var games = gameRepository.Where(x => x.Id == gameId).ToList();
        var team1Id = games.First().Team1Id;
        var team2Id = games.First().Team2Id;
    
        var alignmentsRepository = unitOfWork.Repository<Domain.Entities.AlignedPlayerInGame>();
        
        var playersTeam1 = alignmentsRepository.Where(x => x.GameId == gameId && x.TeamId == team1Id);
        
        if (playersTeam1.IsNullOrEmpty())
            ThrowError("Team1 Alignment not found", StatusCodes.Status404NotFound);

        var playersTeam2 = alignmentsRepository.Where(x => x.GameId == gameId && x.TeamId == team2Id);

        if (playersTeam2.IsNullOrEmpty())
            ThrowError("Team2 Alignment not found", StatusCodes.Status404NotFound);
    }
}