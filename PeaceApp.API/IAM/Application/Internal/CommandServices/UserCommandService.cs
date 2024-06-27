using PeaceApp.API.IAM.Application.Internal.OutboundServices;
using PeaceApp.API.IAM.Domain.Model.Aggregates;
using PeaceApp.API.IAM.Domain.Model.Commands;
using PeaceApp.API.IAM.Domain.Repositories;
using PeaceApp.API.IAM.Domain.Services;
using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.IAM.Application.Internal.CommandServices;

public class UserCommandService (IUserRepository userRepository, IUnitOfWork unitOfWork, 
    ITokenService tokenService, IHashingService hashingService) : IUserCommandService
{
    public  async Task Handle(SignUpCommand command)
    {
        // Confirm if the username already exists
        if (userRepository.ExistsByUserName(command.Username))
        {
            throw new Exception($"Username {command.Username} is already taken");
        }

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error ocurred while creating the user: {e.Message}");
        }
    }

    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindUserByUsernameAsync(command.Username);
        if (user is null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
        {
            throw new Exception("invalid username or password");
        }
        var token = tokenService.GenerateToken(user);
        return (user, token);
    }
}