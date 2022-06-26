using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.SqliteRepository.User
{
    public class UserRepository
    {
        static (string hashed, string salt) Hash(string password)
        {
            byte[] salt = new byte[8];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(salt);
            }
            string hashstring;
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000, HashAlgorithmName.SHA256))
            {
                hashstring = BitConverter.ToString(pbkdf2.GetBytes(32));
            }
            return (hashstring, BitConverter.ToString(salt));
        }
    }
}
