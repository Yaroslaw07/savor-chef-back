using AutoMapper;
using SavorChef.Application.Dtos.Requests;
using SavorChef.Application.Dtos.Responses;
using SavorChef.Domain.Entities;

namespace SavorChef.Application.Mappings;

public class UserMappingProfile: Profile
{
    public UserMappingProfile()
    {
        // Entity to DTO
        CreateMap<UserEntity, UserRequestDto>();
        CreateMap<UserEntity, UserResponseDto>();
        
        // DTO to Entity
        CreateMap<UserRequestDto, UserEntity>();
        CreateMap<UserResponseDto, UserEntity>();
    }
}