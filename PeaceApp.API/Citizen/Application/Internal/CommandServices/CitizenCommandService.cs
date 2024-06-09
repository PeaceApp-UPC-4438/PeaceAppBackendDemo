using PeaceApp.API.Citizen.Domain.Model.Aggregates;
using PeaceApp.API.Citizen.Domain.Model.Commands;
using PeaceApp.API.Citizen.Domain.Repositories;
using PeaceApp.API.Citizen.Domain.Services;
using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.Citizen.Application.Internal.CommandServices;

public class CitizenCommandService(ICitizenRepository profileRepository, IUnitOfWork unitOfWork) : ICitizenCommandService
{
    public async Task<Domain.Model.Aggregates.Citizen?> Handle(CreateCitizenAccountCommand command)
    {
        var citizen = new Domain.Model.Aggregates.Citizen(command);
        try
        {
            await profileRepository.AddAsync(citizen);
            await unitOfWork.CompleteAsync();
            return citizen;
        } catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the profile for Citizen: {e.Message}");
            return null;
        }
    }
}