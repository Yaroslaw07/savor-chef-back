using System.Security.Claims;
using SavorChef.Backend.Data.Dtos;

namespace SavorChef.Backend.Services;

public interface IJWTService
{
    public TokensResponseDto GetTokens(string email);
    public TokensResponseDto RefreshTokens (string refreshToken, string email);
    public ClaimsPrincipal GetClaimsPrincipalFromAccessToken(string accessToken);
    public string? GetCallerEmailFromRequest(HttpRequest httpRequest);

}