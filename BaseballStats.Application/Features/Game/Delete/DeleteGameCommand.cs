using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.Delete;

public class DeleteGameCommand : ICommand<GameDto>
{
    public long GameId { get; set; }
}