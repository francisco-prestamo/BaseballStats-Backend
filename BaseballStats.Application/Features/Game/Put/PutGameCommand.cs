using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.Put;

public record PutGameCommand : GameDto, ICommand<GameDto>
{
}