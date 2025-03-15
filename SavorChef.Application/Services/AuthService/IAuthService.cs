using Microsoft.AspNetCore.Http;
using SavorChef.Application.Dtos.Requests;
using SavorChef.Application.Dtos.Responses;

namespace SavorChef.Application.Services.AuthService;

public interface IAuthService
{
    Task<TokensResponseDto> SignInAsync(LoginRequestDto loginRequest);
    Task<TokensResponseDto> SignUpAsync(RegisterRequestDto registerRequest);
    Task<TokensResponseDto> RefreshTokenAsync(RefreshRequestDto refreshRequest);
}