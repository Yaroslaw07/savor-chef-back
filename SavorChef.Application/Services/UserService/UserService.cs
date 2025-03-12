using AutoMapper;
using SavorChef.Application.Dtos.Requests;
using SavorChef.Application.Dtos.Responses;
using SavorChef.Domain.Entities;
using SavorChef.Infrastructure.Repositories.User;

namespace SavorChef.Application.Services.UserService;

public class UserService: IUserService
{
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return _mapper.Map<UserResponseDto>(user);
        }
    
        public async Task<UserResponseDto> CreateAsync(UserEntity
            userIdDto)
        {
            var user = _mapper.Map<UserEntity>(userIdDto);    
            var createdUser = await _userRepository.CreateAsync(user);
    
            return _mapper.Map<UserResponseDto>(createdUser);
        }
    
        public async Task<UserResponseDto> UpdateAsync(int id, UserRequestDto userIdDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            user.Id = userIdDto.Id;
            var updatedUser = await _userRepository.UpdateAsync(user);
    
            return _mapper.Map<UserResponseDto>(updatedUser);
        }
    
        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(user.Id);
        }
}