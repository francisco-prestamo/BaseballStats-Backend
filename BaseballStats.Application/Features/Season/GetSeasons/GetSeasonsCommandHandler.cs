using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Season.GetSeasons;

public class GetSeasonsCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetSeasonsCommand, List<SeasonDto>>
{
    public override async Task<List<SeasonDto>> ExecuteAsync(GetSeasonsCommand command, CancellationToken cancellationToken = default)
    {
        var seasonRepository = unitOfWork.Repository<Domain.Entities.Season>();

        var seasons = await seasonRepository.GetAllAsync();

        var seasonsDto = seasons.Select(x => x.ToDto());

        return seasonsDto.ToList();
    }
}
