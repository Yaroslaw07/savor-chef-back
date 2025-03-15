namespace SavorChef.Application.Dtos.Responses;

public class RefreshRequestDto
{
    public int UserId { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
}