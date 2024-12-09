using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BaseballStats.Application.Features.Game.GetSubstitutions;

public class GetSubstitutionsCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetSubstitutionsCommand, SubstitutionsDto>
{
    public override async Task<SubstitutionsDto> ExecuteAsync(GetSubstitutionsCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var gameId = command.GameId;

        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();

        var games = gameRepository.Where(x => x.Id == gameId).ToList();
        var team1Id = games.First().Team1Id;
        var team2Id = games.First().Team2Id;
        
        var substitutionsRepository = unitOfWork.Repository<Domain.Entities.Substitution>();

        var substitutionsTeam1 = substitutionsRepository.Where(x => x.GameId == gameId && x.TeamId == team1Id).ToList();  
        var substitutionsTeam2 = substitutionsRepository.Where(x => x.GameId == gameId && x.TeamId == team2Id).ToList();

        var substitutionsDto = (substitutionsTeam1, substitutionsTeam2).ToDto();

        return substitutionsDto;
    }

    private async Task DatabaseValidations(GetSubstitutionsCommand command)
    {
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var game = await gameRepository.GetByIdAsync(command.GameId);

        if (game is null)
            ThrowError("GameId not found", StatusCodes.Status404NotFound);
    }
}