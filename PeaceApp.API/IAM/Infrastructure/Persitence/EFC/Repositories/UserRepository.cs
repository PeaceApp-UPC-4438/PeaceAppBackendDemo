using PeaceApp.API.IAM.Domain.Model.Aggregates;
using PeaceApp.API.IAM.Domain.Repositories;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PeaceApp.API.IAM.Infrastructure.Persitence.EFC.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public Task<User?> FindUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public bool ExistsByUserName(string username)
    {
        throw new NotImplementedException();
    }
}