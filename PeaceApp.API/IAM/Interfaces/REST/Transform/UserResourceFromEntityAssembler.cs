using PeaceApp.API.IAM.Domain.Model.Aggregates;
using PeaceApp.API.IAM.Interfaces.REST.Resources;

namespace PeaceApp.API.IAM.Interfaces.REST.Transform;

public class UserResourceFromEntityAssembler
{
     public static UserResource ToResourceFromEntity(User entity)
     {
          return new UserResource(entity.Id, entity.Username);
     }
}