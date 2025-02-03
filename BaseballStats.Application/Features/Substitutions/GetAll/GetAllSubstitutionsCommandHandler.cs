using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.Substitutions.GetAll;

public class GetAllSubstitutionsCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetAllSubstitutionsCommand, List<SingleSubstitutionCRUDDto>>
{
    public override async Task<List<SingleSubstitutionCRUDDto>> ExecuteAsync(GetAllSubstitutionsCommand command, CancellationToken ct = default)
    {
        var substitutionsRepository = unitOfWork.Repository<Domain.Entities.Substitution>();
        var substitutions = (await substitutionsRepository.GetAllAsync()).ToList();

        return substitutions.Select(x => x.ToCRUDDto()).ToList();
    }
}