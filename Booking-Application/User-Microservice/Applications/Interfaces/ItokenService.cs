
namespace User_Microservice.Applications.Interfaces;

using System.Collections.Generic;
using System.Security.Claims;

public interface ITokenService
{
    string GenerateToken(string userId, string username, IEnumerable<Claim> extraClaims = null);
}
