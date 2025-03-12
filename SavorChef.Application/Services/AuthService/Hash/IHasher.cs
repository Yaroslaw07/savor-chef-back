namespace SavorChef.Application.Services.AuthService.Hash;

public interface IHasher
{
    string Hash(string input);
}