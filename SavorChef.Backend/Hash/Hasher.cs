using System.Security.Cryptography;
using System.Text;

namespace SavorChef.Backend.Hash;

public class Hasher : IHasher
{
    private readonly string _salt;

    public Hasher(string salt)
    {
        _salt = salt;
    }

    public string Hash(string input)
    {
        using HashAlgorithm sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input + _salt));
        var stringBuilder = new StringBuilder();
        foreach (var b in bytes)
        {
            stringBuilder.Append(b.ToString("X2"));
        }

        return stringBuilder.ToString();
    }

}