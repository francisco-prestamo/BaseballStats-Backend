using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Player.Post;

public class PostPlayerCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostPlayerCommand, RegularPlayerDto>
{
    public override async Task<RegularPlayerDto> ExecuteAsync(PostPlayerCommand command, CancellationToken cancellationToken = default)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();

        var entity = new Domain.Entities.Player() {
            Name = command.Name,
            Age = command.Age,
            YearsOfExperience = command.YearsOfExperience,
            BattingAverage = command.BattingAverage
        };
        var createdPlayer = await playerRepository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return createdPlayer.ToDto();
    }
}