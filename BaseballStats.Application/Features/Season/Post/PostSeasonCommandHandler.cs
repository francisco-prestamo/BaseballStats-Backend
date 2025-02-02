using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Season.Post;

public class PostSeasonCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostSeasonCommand, SeasonDto>
{
    public override async Task<SeasonDto> ExecuteAsync(PostSeasonCommand command, CancellationToken ct = new CancellationToken())
    {
        var seasonRepository = unitOfWork.Repository<Domain.Entities.Season>();

        var validate = await seasonRepository.GetByIdAsync(command.Id);

        if (validate is not null)
            ThrowError("Season already exists", StatusCodes.Status409Conflict);

        var season = await seasonRepository.AddAsync(new Domain.Entities.Season() { Id = command.Id });

        await unitOfWork.SaveChangesAsync(ct);

        return season.ToDto();
    }
}