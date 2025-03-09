using SavorChef.Domain.Entities;

namespace SavorChef.Infrastructure.Repositories.User;

public interface IUserRepository
{
    public Task<UserEntity> GetUserAsync(int id);
    
    public  Task<UserEntity> CreateUserAsync(UserEntity user);
    
    public Task<UserEntity> UpdateUserAsync(UserEntity user);
    
    public Task DeleteUserAsync(int id);
}