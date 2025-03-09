using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using SavorChef.Application.Dtos.Responses;

namespace SavorChef.Application.Services;

public class JWTService : IJWTService
{
    private readonly Dictionary<string, string> _refreshTokens = new();

    public TokensResponseDto GetTokens(string email)
    {
        var claims = new[]
        {
            new Claim("Email", email)
        };
        var jwt = new JwtSecurityToken(
            "123",
            "31231",
            claims,
            expires: DateTime.Now.AddSeconds(600),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret12345678abcdef")),
                SecurityAlgorithms.HmacSha256Signature));
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
        var refreshToken = new Random().Next() + "refresh";
        _refreshTokens.Add(refreshToken, email);
        return new TokensResponseDto { AccessToken = accessToken, RefreshToken = refreshToken };
    }

    public TokensResponseDto RefreshTokens(string refreshToken, string email)
    {
        if (!_refreshTokens.ContainsKey(refreshToken)) return null;

        if (_refreshTokens[refreshToken] != email) return null;

        _refreshTokens.Remove(refreshToken);
        return GetTokens(email);
    }

    public ClaimsPrincipal GetClaimsPrincipalFromAccessToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = "31231",
            ValidateIssuer = true,
            ValidIssuer = "123",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret12345678abcdef")),
            ValidateLifetime = true
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var validatedToken);
        if (validatedToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,
                StringComparison.InvariantCultureIgnoreCase))
            return null;

        return principal;
    }

    public string? GetCallerEmailFromRequest(HttpRequest httpRequest)
    {
        var authorizationHeader = httpRequest.Headers[HeaderNames.Authorization];
        if (authorizationHeader.Count == 0) return null;

        var bearer = authorizationHeader[0];
        if (!bearer.Contains("Bearer ")) return null;

        var accessToken = bearer.Replace("Bearer ", "");
        var claimsPrincipal = GetClaimsPrincipalFromAccessToken(accessToken);
        return claimsPrincipal.FindFirst("Email").Value;
    }

    public string? GetAccessTokenFromRequest(HttpRequest httpRequest)
    {
        var authorizationHeader = httpRequest.Headers[HeaderNames.Authorization];
        if (authorizationHeader.Count == 0) return null;

        var bearer = authorizationHeader[0];
        if (!bearer.Contains("Bearer ")) return null;

        var accessToken = bearer.Replace("Bearer ", "");
        return accessToken;
    }
}