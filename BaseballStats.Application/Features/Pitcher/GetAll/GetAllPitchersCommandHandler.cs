using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.Pitcher.GetAll;

public class GetAllPitchersCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetAllPitchersCommand, List<PitcherDto>>
{
    public override async Task<List<PitcherDto>> ExecuteAsync(GetAllPitchersCommand command, CancellationToken cancellationToken = default)
    {
        var pitcher_table = unitOfWork.Repository<Domain.Entities.Pitcher>().DbSet;
        var player_table = unitOfWork.Repository<Domain.Entities.Player>().DbSet;

        var entities = (
          from pit in pitcher_table
          join p in player_table on pit.Id equals p.Id
          select new Domain.Entities.Pitcher()
          {
              Id = pit.Id,
              Player = p,
              GamesWonNumber = pit.GamesWonNumber,
              GamesLostNumber = pit.GamesLostNumber,
              RightHanded = pit.RightHanded,
              AllowedRunsAvg = pit.AllowedRunsAvg
          }
        );

        return await Task.FromResult(entities.Select(x => x.ToDto()).ToList());
    }
}