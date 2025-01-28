using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.GetAll;

public record GetAllGamesCommand : ICommand<List<GameDto>>;
