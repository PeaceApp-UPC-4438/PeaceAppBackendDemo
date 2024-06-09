using PeaceApp.API.Organization.Domain.Model.Aggregates;
using PeaceApp.API.Organization.Domain.Model.Commands;
using PeaceApp.API.Organization.Domain.Repositories;
using PeaceApp.API.Organization.Domain.Services;
using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.Organization.Application.Internal.CommandServices;

public class OrganizationAccountCommandService : IOrganizationAccountCommandService
{
    private readonly IOrganizationAccountRepository _organizationAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrganizationAccountCommandService(IOrganizationAccountRepository organizationAccountRepository, IUnitOfWork unitOfWork)
    {
        _organizationAccountRepository = organizationAccountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrganizationAccount> Handle(CreateOrganizationAccountCommand command)
    {
        var organizationAccount = await _organizationAccountRepository.FindByOrganizationNameAsync(
            command.OrganizationName);
        if (organizationAccount != null)
            throw new Exception("Organization Account with this name  already exisits");
        organizationAccount = new OrganizationAccount(command);
        await _organizationAccountRepository.AddAsync(organizationAccount);
        await _unitOfWork.CompleteAsync();
        return organizationAccount;
    }
}