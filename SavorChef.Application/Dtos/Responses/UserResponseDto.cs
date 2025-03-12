using System.ComponentModel.DataAnnotations;
using SavorChef.Domain.Entities;

namespace SavorChef.Application.Dtos.Responses;

public class UserResponseDto
{
    public int Id { get; set; }
    
    public string Email { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;
}