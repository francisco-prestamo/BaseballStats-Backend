using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Player.Create;

public class CreatePlayerCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<CreatePlayerCommand, RegularPlayerDto>
{
    public override async Task<RegularPlayerDto> ExecuteAsync(CreatePlayerCommand command, CancellationToken cancellationToken = default)
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