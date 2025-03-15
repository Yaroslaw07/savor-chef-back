using SavorChef.Domain.Entities;

namespace SavorChef.Infrastructure.Repositories.User;

public interface IUserRepository
{
    public Task<UserEntity> GetByIdAsync(int id);
    
    public Task<UserEntity> GetByEmailAsync(string email);
    
    public  Task<UserEntity> CreateAsync(UserEntity user);
    
    public Task<UserEntity> UpdateAsync(UserEntity user);
    
    public Task DeleteAsync(int id);
}