using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using SavorChef.Backend.Data.Entities;

namespace SavorChef.Backend.Repositories;

public interface IUserRepository 
{
    public UserEntity GetUser(string email);


    public UserEntity CreateUser(UserEntity user);


    public void DeleteUser(string email);

    //update
}