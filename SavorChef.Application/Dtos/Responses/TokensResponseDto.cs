namespace SavorChef.Application.Dtos.Responses;

public class TokensResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}