using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SavorChef.Application.Dtos.Requests;
using SavorChef.Application.Dtos.Responses;
using SavorChef.Domain.Entities;
using SavorChef.Domain.Exceptions;
using SavorChef.Infrastructure.Repositories.User;

namespace SavorChef.Application.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;
    
    public AuthService(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<TokensResponseDto> SignInAsync(LoginRequestDto loginRequestDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginRequestDto.Email) ?? throw AuthExceptions.Authentication.InvalidCredentials();

        var isValidPassword = _passwordHasher.VerifyPassword(loginRequestDto.Password, user.Password);
        if (!isValidPassword) 
            throw AuthExceptions.Authentication.InvalidCredentials();

        return GenerateTokens(user);
    }
    
    public async Task<TokensResponseDto> SignUpAsync(RegisterRequestDto registerRequestDto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(registerRequestDto.Email);
        if (existingUser != null)
            throw AuthExceptions.Registration.EmailTaken(registerRequestDto.Email);
        
        var newUser = _mapper.Map<UserEntity>(registerRequestDto);
        newUser.Password = _passwordHasher.HashPassword(registerRequestDto.Password);

        try
        {
            var createdUser = await _userRepository.CreateAsync(newUser);
            return GenerateTokens(createdUser);
        }
        catch (Exception e)
        {
            throw AuthExceptions.Registration.Failed(e);
        }
    }
    
    public async Task<TokensResponseDto> RefreshTokenAsync(RefreshRequestDto refreshRequestDto)
    {
        var refreshToken = refreshRequestDto.RefreshToken;
        if (string.IsNullOrEmpty(refreshToken))
            throw AuthExceptions.Authorization.InvalidRefreshToken();
        
        var principal = ValidateToken(refreshToken, true);
        if (principal == null)
            throw AuthExceptions.Authorization.InvalidRefreshToken();

        var id = refreshRequestDto.UserId;
        
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw UserExceptions.NotFound.ById(id);

        return GenerateTokens(user);
    }
    
    private TokensResponseDto GenerateTokens(UserEntity user)
    {
        var key = GetSecretKey();

        var accessToken = CreateToken(
            user,
            key,
            TimeSpan.FromMinutes(15),
            false);

        var refreshToken = CreateToken(
            user,
            key,
            TimeSpan.FromDays(7),
            true);

        return new TokensResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            UserId = user.Id
        };
    }
    
    private string CreateToken(UserEntity user, byte[] key, TimeSpan expiry, bool isRefreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        // Add more claims for access token
        if (!isRefreshToken)
        {
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
        }
        else
        {
            claims.Add(new Claim("token_type", "refresh"));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(expiry),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private ClaimsPrincipal? ValidateToken(string token, bool isRefreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = GetSecretKey();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            
            // If validating refresh token, ensure the token_type claim is present
            if (isRefreshToken)
            {
                var tokenTypeClaim = principal.FindFirst("token_type");
                if (tokenTypeClaim == null || tokenTypeClaim.Value != "refresh")
                    return null;
            }

            return principal;
        }
        catch
        {
            return null;
        }
    }

    private byte[] GetSecretKey()
    {
        var secret = _configuration["JWT:Secret"] ?? 
                     throw AuthExceptions.Configuration.MissingJwtSecret();
        return Encoding.ASCII.GetBytes(secret);
    }
}