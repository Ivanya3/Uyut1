using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHasher
{
    // Генерирует хеш пароля (с солью)
    public static string HashPassword(string password)
    {
        // Генерируем случайную "соль"
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

        // Создаём хеш с помощью PBKDF2
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(20);

        // Комбинируем соль и хеш
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        // Конвертируем в строку Base64
        return Convert.ToBase64String(hashBytes);
    }

    // Проверяет пароль против хеша
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Декодируем строку Base64 обратно в байты
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        // Извлекаем соль (первые 16 байт)
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        // Хешируем введённый пароль с той же солью
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(20);

        // Сравниваем хеши
        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 16] != hash[i])
                return false;
        }
        return true;
    }
}