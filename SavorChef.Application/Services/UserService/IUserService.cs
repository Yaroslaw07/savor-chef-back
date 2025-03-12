using SavorChef.Application.Dtos.Requests;
using SavorChef.Application.Dtos.Responses;
using SavorChef.Domain.Entities;

namespace SavorChef.Application.Services.UserService;

public interface IUserService
{
    Task<UserResponseDto> GetByIdAsync(int id);
    Task<UserResponseDto> CreateAsync(UserEntity userDto);
    Task<UserResponseDto> UpdateAsync(int id, UserRequestDto userIdDto);
    Task DeleteAsync(int id);
}