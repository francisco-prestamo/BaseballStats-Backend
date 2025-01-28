using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.TechnicalDirector.GetAll;

public class GetAllTechnicalDirectorsCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetAllTechnicalDirectorsCommand, List<RegisteredUserDto>>
{
    public override async Task<List<RegisteredUserDto>> ExecuteAsync(GetAllTechnicalDirectorsCommand command, CancellationToken ct = default)
    {
        var technicalDirectorRepository = unitOfWork.Repository<Domain.Entities.Identity.TechnicalDirector>();

        var technicalDirectors = await technicalDirectorRepository.GetAllAsync();

        return technicalDirectors.Select(x => x.ToDto()).ToList();        
    }

}