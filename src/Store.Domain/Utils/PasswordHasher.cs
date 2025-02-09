using System.Security.Cryptography;

namespace Store.Domain.Utils;

public class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100;//_000;

    private readonly string _password;
    
    public PasswordHasher(string password)
    {
        _password = password;
    }
    
    public string Hash { get; private set; }
    public string Salt { get; private set; }
    
    public void HashPassword(string password)
    {
        var salt = GenerateSalt(SaltSize);
        var hash = PBKDF2Hash(password, salt);
        Salt = Convert.ToBase64String(salt);
        Hash = Convert.ToBase64String(hash);
    }

    public static bool VerifyPassword(
        string password, string storedHash, string storedSalt)
    {
        var salt = Convert.FromBase64String(storedSalt);
        var hash = PBKDF2Hash(password, salt);
        
        return Convert.ToBase64String(hash) == storedHash;
    }

    private static byte[] GenerateSalt(int length)
    {
        var salt = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        
        return salt;
    }

    private static byte[] PBKDF2Hash(string password, byte[] salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(
            password, salt, Iterations, HashAlgorithmName.SHA256);
        
        return pbkdf2.GetBytes(HashSize);
    }
}
