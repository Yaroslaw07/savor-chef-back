namespace SavorChef.Backend.Data.Dtos;

public class TokensResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}