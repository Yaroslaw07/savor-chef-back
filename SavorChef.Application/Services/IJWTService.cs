using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SavorChef.Application.Dtos.Responses;


namespace SavorChef.Application.Services;

public interface IJWTService
{
    public TokensResponseDto GetTokens(string email);
    public TokensResponseDto RefreshTokens(string refreshToken, string email);
    public ClaimsPrincipal GetClaimsPrincipalFromAccessToken(string accessToken);
    public string? GetCallerEmailFromRequest(HttpRequest httpRequest);
}