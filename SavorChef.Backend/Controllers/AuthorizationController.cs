using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SavorChef.Backend.Data.Dtos;
using SavorChef.Backend.Data.Entities;
using SavorChef.Backend.Repositories;
using SavorChef.Backend.Services;

namespace SavorChef.Backend.Controllers;

public class AuthorizationController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IJWTService _jwtService;

    public AuthorizationController(IUserRepository userRepository, IJWTService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    [HttpPost]
    [Route("/signin")]
    public IActionResult SignIn([FromBody] LoginRequestDto loginRequestDto)
    {
        var user = _userRepository.GetUser(loginRequestDto.Email);

        if (user == null)
        {
            return new BadRequestResult();
        }

        if (loginRequestDto.Password != user.Password)
        {
            return new BadRequestResult();
        }

        var response = _jwtService.GetTokens(user.Email);

        return new OkObjectResult(response);
    }

    [HttpPost]
    [Route("/signup")]
    public IActionResult SignUp([FromBody] RegisterRequestDto registerRequestDto)
    {
        if (_userRepository.GetUser(registerRequestDto.Email )!= null)
        {
            return new BadRequestResult();
        }

        var user = new UserEntity
        {
            Email = registerRequestDto.Email,
            Password = registerRequestDto.Password
        };
        var createdUser =_userRepository.CreateUser(user);
        return new OkObjectResult(createdUser); 
    }

    [HttpPost]
    [Route("/refresh")]
    public IActionResult Refresh([FromBody] RefreshRequestDto refreshRequestDto)
    {
        var authorizationHeader = Request.Headers[HeaderNames.Authorization];
        if (authorizationHeader.Count == 0)
        {
            return new UnauthorizedObjectResult("Invalid token.");
        }

        var bearer = authorizationHeader[0];
        if (!bearer.Contains("Bearer "))
        {
            return new UnauthorizedObjectResult("Invalid token.");
        }

        var accessToken = bearer.Replace("Bearer ", "");
        var principal = _jwtService.GetClaimsPrincipalFromAccessToken(accessToken);
        var email = principal.FindFirstValue("Email");

        if (email == null)
        {
            return new UnauthorizedObjectResult("Invalid token.");
        }

        var tokensResponseDto = _jwtService.RefreshTokens(refreshRequestDto.RefreshToken, email);

        if (tokensResponseDto == null)
        {
            return new UnauthorizedObjectResult("Invalid token.");
        }

        return new OkObjectResult(tokensResponseDto);
    }
}   