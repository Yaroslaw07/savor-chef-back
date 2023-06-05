using SavorChef.Backend.Data;
using SavorChef.Backend.Data.Entities;

namespace SavorChef.Backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApiContext _apiContext;

    public UserRepository(ApiContext apiContext)
    {
        _apiContext = apiContext;
    }

    public UserEntity GetUser(string email)
    {
        return _apiContext.Users.FirstOrDefault(x => x.Email == email);
    }

    public UserEntity CreateUser(UserEntity user)
    {
        var userEntity = _apiContext.Users.Add(user);
        _apiContext.SaveChanges();
        return userEntity.Entity;
    }

    public void DeleteUser(string email)
    {
        var result = GetUser(email);

        if (result != null)
        {
            _apiContext.Users.Remove(result);
            _apiContext.SaveChanges();
        }
    }
}