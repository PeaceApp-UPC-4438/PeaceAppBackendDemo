using PeaceApp.API.IAM.Domain.Model.Aggregates;
using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindUserByUsernameAsync(string username);
    bool ExistsByUserName(string username);
    
}