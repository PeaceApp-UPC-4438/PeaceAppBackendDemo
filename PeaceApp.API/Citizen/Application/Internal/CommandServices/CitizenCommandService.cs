using PeaceApp.API.Citizen.Domain.Model.Aggregates;
using PeaceApp.API.Citizen.Domain.Model.Commands;
using PeaceApp.API.Citizen.Domain.Model.ValueObjects;
using PeaceApp.API.Citizen.Domain.Repositories;
using PeaceApp.API.Citizen.Domain.Services;
using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.Citizen.Application.Internal.CommandServices;

public class CitizenCommandService(ICitizenRepository citizenRepository, IUnitOfWork unitOfWork) : ICitizenCommandService
{
    public async Task<Domain.Model.Aggregates.Citizen?> Handle(CreateCitizenAccountCommand command)
    {
        var existingCitizen = await citizenRepository.FindCitizenByEmailAsync(new EmailAddress(command.Email));
        if (existingCitizen != null)
        {
            throw new InvalidOperationException($"A citizen with the email {command.Email} already exists.");
        }
        var citizen = new Domain.Model.Aggregates.Citizen(command);
        try
        {
            await citizenRepository.AddAsync(citizen);
            await unitOfWork.CompleteAsync();
            return citizen;
        } 
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the profile for Citizen: {e.Message}");
            return null;
        }
    }
    public async Task<Domain.Model.Aggregates.Citizen?> Handle(UpdateCitizenAccountCommand command)
    {
        var citizen = await citizenRepository.GetByIdAsync(command.Id);
        if (citizen == null) return null;

        citizen.UpdateName(command.FirstName, command.LastName);
        citizen.UpdateEmail(command.Email);
        citizen.UpdateAddress(command.Street, command.Number, command.City, command.PostalCode, command.Country);

        try
        {
            await citizenRepository.UpdateAsync(citizen);
            await unitOfWork.CompleteAsync();
            return citizen;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the profile for Citizen: {e.Message}");
            return null;
        }
    }

    public async Task<bool> Handle(DeleteCitizenAccountCommand command)
    {
        var citizen = await citizenRepository.GetByIdAsync(command.Id);
        if (citizen == null) return false;

        try
        {
            await citizenRepository.DeleteAsync(citizen);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while deleting the profile for Citizen: {e.Message}");
            return false;
        }
    }
}
