using AutoMapper;
using SavorChef.Application.Dtos.Requests;
using SavorChef.Application.Dtos.Responses;
using SavorChef.Domain.Entities;

namespace SavorChef.Application.Mappings;
public class RecipeMappingProfile : Profile
{
    public RecipeMappingProfile()
    {
        // Entity to DTO
        CreateMap<RecipeEntity, RecipeRequestDto>();
        CreateMap<RecipeEntity, RecipeResponseDto>();

        // DTO to Entity
        CreateMap<RecipeRequestDto, RecipeEntity>();
        CreateMap<RecipeResponseDto, RecipeEntity>();
    }
}