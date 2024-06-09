
using PeaceApp.API.Citizen.Domain.Model.Queries;

namespace PeaceApp.API.Citizen.Domain.Services;

public interface ICitizenQueryService
{
    Task<IEnumerable<Model.Aggregates.Citizen>> Handle(GetAllCitizensQuery query);
    Task<Model.Aggregates.Citizen?> Handle(GetCitizenByEmailQuery query);
    Task<Model.Aggregates.Citizen?> Handle(GetCitizenByIdQuery query);
}