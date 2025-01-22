using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Entities.Identity;
using FastEndpoints;

namespace BaseballStats.Application.Features.Generate;

public record GenerateDataCommand : ICommand<EmptyResponse>
{
    
}