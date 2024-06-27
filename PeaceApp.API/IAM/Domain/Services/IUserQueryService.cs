using PeaceApp.API.IAM.Domain.Model.Aggregates;
using PeaceApp.API.IAM.Domain.Model.Queries;

namespace PeaceApp.API.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<User?> Handle(GetUserByUsernameQuery query);
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
}