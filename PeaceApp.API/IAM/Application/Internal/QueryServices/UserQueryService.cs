using PeaceApp.API.IAM.Domain.Model.Aggregates;
using PeaceApp.API.IAM.Domain.Model.Queries;
using PeaceApp.API.IAM.Domain.Repositories;
using PeaceApp.API.IAM.Domain.Services;

namespace PeaceApp.API.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }

    public async Task<User?> Handle(GetUserByUsernameQuery query)
    {
        return await userRepository.FindUserByUsernameAsync(query.Username);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }
}
