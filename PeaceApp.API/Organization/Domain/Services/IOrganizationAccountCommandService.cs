using PeaceApp.API.Organization.Domain.Model.Aggregates;
using PeaceApp.API.Organization.Domain.Model.Commands;

namespace PeaceApp.API.Organization.Domain.Services;

public interface IOrganizationAccountCommandService
{
    Task<OrganizationAccount> Handle(CreateOrganizationAccountCommand command);
}