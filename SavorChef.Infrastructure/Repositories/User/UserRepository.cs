using Microsoft.EntityFrameworkCore;
using SavorChef.Domain.Entities;
using SavorChef.Domain.Exceptions;

namespace SavorChef.Infrastructure.Repositories.User;

    public class UserRepository(DataContext dataContext) : IUserRepository
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task<UserEntity> GetUserAsync(int id)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id)
                   ?? throw UserExceptions.NotFound.ById(id);
        }

        public async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            var userEntity = await _dataContext.Users.AddAsync(user);

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw UserExceptions.Create.Failed(e);
            }

            return userEntity?.Entity ?? throw UserExceptions.Create.Failed();
        }

        public async Task<UserEntity> UpdateUserAsync(UserEntity user)
        {
            var result = await GetUserAsync(user.Id);

            try
            {
                _dataContext.Entry(result).CurrentValues.SetValues(user);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw UserExceptions.Update.Failed(user.Id, e);
            }

            return result;
        }

        public async Task DeleteUserAsync(int id)
        {
            var result = await GetUserAsync(id);

            try
            {
                _dataContext.Users.Remove(result);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw UserExceptions.Update.Failed(id, e);
            }
        }
    }
