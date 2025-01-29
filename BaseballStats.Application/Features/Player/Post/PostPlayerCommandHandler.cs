using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Application.DTOs;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Player.Post;

public class PostPlayerCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostPlayerCommand, RegularPlayerDto>
{
    public override async Task<RegularPlayerDto> ExecuteAsync(PostPlayerCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();

        var entity = new Domain.Entities.Player() {
            Id = command.Id,
            Name = command.Name,
            Age = command.Age,
            YearsOfExperience = command.YearsOfExperience,
            BattingAverage = command.BattingAverage
        };
        var createdPlayer = await playerRepository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return createdPlayer.ToDto();
    }

    private async Task DatabaseValidations(PostPlayerCommand command)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.FirstOrDefaultAsync(x => x.Id == command.Id);

        if (player is not null)
            ThrowError("Player with given Id exists", StatusCodes.Status400BadRequest);
    }
}