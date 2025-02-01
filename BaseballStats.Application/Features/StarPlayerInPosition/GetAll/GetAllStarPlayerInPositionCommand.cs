using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.StarPlayerInPosition.GetAll;

public class GetAllStarPlayerInPositionCommand : ICommand<List<StarPlayerInPositionDto>>
{}
