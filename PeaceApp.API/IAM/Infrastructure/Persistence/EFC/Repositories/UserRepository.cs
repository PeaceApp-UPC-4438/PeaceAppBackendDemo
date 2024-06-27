using Microsoft.EntityFrameworkCore;
using PeaceApp.API.IAM.Domain.Model.Aggregates;
using PeaceApp.API.IAM.Domain.Repositories;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PeaceApp.API.IAM.Infrastructure.Persitence.EFC.Repositories;
// OJITO
public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindUserByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }

    public bool ExistsByUserName(string username)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username));
    }
}