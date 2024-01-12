using System.Security.Cryptography;

namespace TTP.Common.Helpers;

public static class PasswordHashHelper
{
    private const int SaltSize = 16; // Tamaño del salt en bytes
    private const int HashSize = 32; // Tamaño del hash en bytes
    private const int Iterations = 10000; // Número de iteraciones para la función de hash
    
    public static string HashPassword(string password)
    {
        var salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
        {
            var hash = pbkdf2.GetBytes(HashSize);

            var saltPlusHash = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, saltPlusHash, 0, SaltSize);
            Array.Copy(hash, 0, saltPlusHash, SaltSize, HashSize);

            var hashedPassword = Convert.ToBase64String(saltPlusHash);
            return hashedPassword;
        }
    }
    
    public static bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var saltPlusHash = Convert.FromBase64String(hashedPassword);
        var salt = new byte[SaltSize];
        Array.Copy(saltPlusHash, 0, salt, 0, SaltSize);

        using (var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, salt, Iterations))
        {
            var hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hash[i] != saltPlusHash[i + SaltSize])
                {
                    return false;
                }
            }
            return true;
        }
    }
}