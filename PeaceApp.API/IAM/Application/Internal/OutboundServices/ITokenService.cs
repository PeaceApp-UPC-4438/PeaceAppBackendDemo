using PeaceApp.API.IAM.Domain.Model.Aggregates;

namespace PeaceApp.API.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}