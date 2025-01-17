using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.Post;

public class PostGameCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostGameCommand, PostGameResponse>
{
    public override async Task<PostGameResponse> ExecuteAsync(PostGameCommand command, CancellationToken ct = new CancellationToken())
    {
        var repository = unitOfWork.Repository<Domain.Entities.Game>();

        var game = new Domain.Entities.Game
        {
            Team1Id = command.Team1Id,
            Team2Id = command.Team2Id,
            Date = command.Date,
            Winner1 = command.WinTeam,
            Runs1 = command.Team1Runs,
            Runs2 = command.Team2Runs,
            SeriesId = command.SeriesId,
        };

        await repository.AddAsync(game);
        await unitOfWork.SaveChangesAsync(ct);

        return new PostGameResponse
        {
            Id = game.Id,
            Team1Id = game.Team1Id,
            Team2Id = game.Team2Id,
            Date = game.Date,
            WinTeam = game.Winner1,
            Team1Runs = game.Runs1,
            Team2Runs = game.Runs2,
            SeriesId = game.SeriesId,
        };
    }
}