using System;
using System.Security.Cryptography;
using Thimble.Credentials.AWS.Dynamo.Credentials.Exceptions;

namespace Thimble.Credentials.AWS.Dynamo.Credentials.Helpers
{
    public static class PasswordHashHelper
    {
        public static string Generate(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }
        
        public static bool Verify(string hashPassword, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(hashPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    throw new InvalidEmailOrPasswordException();
                }   
            }

            return true;
        }
    }
}