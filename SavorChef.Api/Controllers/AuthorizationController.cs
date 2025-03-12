using Microsoft.AspNetCore.Mvc;
using SavorChef.Application.Dtos.Requests;
using SavorChef.Application.Dtos.Responses;
using SavorChef.Application.Services.Hash;
using SavorChef.Application.Services.Jwt;
using SavorChef.Domain.Entities;
using SavorChef.Infrastructure.Repositories.User;

namespace SavorChef.Api.Controllers;

public class AuthorizationController : ControllerBase
{
    private readonly IHasher _hasher = new Hasher("salt_here");
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;

    public AuthorizationController(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    [HttpPost]
    [Route("/signin")]
    public IActionResult SignIn([FromBody] LoginRequestDto loginRequestDto)
    {
        // TO DO: var user = _userRepository.GetUserA(loginRequestDto.Email);

        if (user == null) return new BadRequestResult();

        if (_hasher.Hash(loginRequestDto.Password) != user.Password) return new BadRequestResult();

        var response = _jwtService.GetTokens(user.Email);

        return new OkObjectResult(response);
    }

    [HttpPost]
    [Route("/signup")]
    public IActionResult SignUp([FromBody] RegisterRequestDto registerRequestDto)
    {
        if (_userRepository.GetUser(registerRequestDto.Email) != null) return new BadRequestResult();

        var hashedPassword = _hasher.Hash(registerRequestDto.Password);
        var user = new UserEntity
        {
            UserName = registerRequestDto.UserName,
            Email = registerRequestDto.Email,
            Password = hashedPassword
        };
        var createdUser = _userRepository.CreateUser(user);
        return new OkResult();
    }

    [HttpPost]
    [Route("/refresh")]
    public IActionResult Refresh([FromBody] RefreshRequestDto refreshRequestDto)
    {
        var email = _jwtService.GetCallerEmailFromRequest(Request);

        if (email == null) return new UnauthorizedObjectResult("Invalid token.");

        var tokensResponseDto = _jwtService.RefreshTokens(refreshRequestDto.RefreshToken, email);

        if (tokensResponseDto == null) return new UnauthorizedObjectResult("Invalid token.");

        return new OkObjectResult(tokensResponseDto);
    }
}