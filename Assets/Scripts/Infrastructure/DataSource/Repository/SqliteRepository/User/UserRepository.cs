using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using Dapper;
using Yaroyan.Game.RPG.Domain.Model.User;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.SqliteRepository.User
{
    public class UserRepository : Repository<UserId, Domain.Model.User.User>, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction) { }

        static (string Hashed, string Salt) Hash(string password)
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

        static string Hash(string password, string salt)
        {
            byte[] saltbytes = salt.Split("-").Select(e => Convert.ToByte(e, 16)).ToArray();
            string hashstring;
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltbytes, 1000, HashAlgorithmName.SHA256))
            {
                hashstring = BitConverter.ToString(pbkdf2.GetBytes(32));
            }
            return hashstring;
        }

        public override UserId NextIdentity()
        {
            throw new NotImplementedException();
        }

        public override void Save(Domain.Model.User.User aggregateRoot)
        {
            string sql = $"insert into {TableName} (id, password, salt) values (@id, @password, @salt)";
            (string Hashed, string Salt) result = Hash(aggregateRoot.Password.Word);
            object param = new { id = aggregateRoot.Id, password = result.Hashed, salt = result.Salt };
            Connection.Execute(sql, param, Transaction);
        }

        public override Domain.Model.User.User Find(Domain.Model.User.User aggregateRoot)
        {
            dynamic result = Connection.QueryFirstOrDefault($"select * from {TableName} where id = @id", new { id = aggregateRoot.Id }, Transaction);
            return result.password?.Equals(Hash(aggregateRoot.Password.Word, result.salt)) ?? false
                ? new Domain.Model.User.User(new UserId(result.id), new Password(aggregateRoot.Password.Word))
                : null;
        }

    }
}
