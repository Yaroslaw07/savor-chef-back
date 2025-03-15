using Microsoft.AspNetCore.Mvc;
using SavorChef.Application.Dtos.Requests;
using SavorChef.Application.Dtos.Responses;
using SavorChef.Application.Services.AuthService;

namespace SavorChef.Api.Controllers;

public class AuthorizationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthorizationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginRequestDto loginRequestDto)
    {
        var response = await _authService.SignInAsync(loginRequestDto);
        
        return new OkObjectResult(response);
    }

    [HttpPost]
    [Route("/signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterRequestDto registerRequestDto)
    {
        var response = await _authService.SignUpAsync(registerRequestDto);

        return new OkObjectResult(response);
    }

    [HttpPost]
    [Route("/refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto refreshRequestDto)
    {
        var response = await _authService.RefreshTokenAsync(refreshRequestDto);

        return new OkObjectResult(response);
    }
}