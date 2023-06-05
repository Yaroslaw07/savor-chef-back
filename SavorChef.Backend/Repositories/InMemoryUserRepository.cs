using SavorChef.Backend.Data.Entities;

namespace SavorChef.Backend.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<UserEntity> _userEntities = new()
    {
        new() { Email = "string", Password = "string" }
    };

    public UserEntity GetUser(string email)
    {
        foreach (var user in _userEntities)
            if (user.Email == email)
                return user;

        return null;
    }

    public UserEntity CreateUser(UserEntity user)
    {
        if (GetUser(user.Email) != null) return user;
        _userEntities.Add(user);
        return user;
    }

    public void DeleteUser(string email)
    {
        var user = GetUser(email);
        if (user != null) _userEntities.Remove(user);
    }
}