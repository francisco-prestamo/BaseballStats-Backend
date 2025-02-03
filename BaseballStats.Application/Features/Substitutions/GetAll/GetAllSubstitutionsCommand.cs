using FastEndpoints;
using BaseballStats.Application.DTOs;

namespace BaseballStats.Application.Features.Substitutions.GetAll;

public class GetAllSubstitutionsCommand : ICommand<List<SingleSubstitutionCRUDDto>>
{}
