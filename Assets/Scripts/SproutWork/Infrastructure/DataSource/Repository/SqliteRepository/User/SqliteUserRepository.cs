using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using Dapper;
using Yaroyan.SproutWork.Domain.Model.User;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Repository.SqliteRepository.User
{
    public class SqliteUserRepository : Repository<UserId, Domain.Model.User.User>, IUserRepository
    {
        public SqliteUserRepository(IDbTransaction transaction) : base(transaction) { }

        /// <summary>
        /// Hash the password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Hash the password with the specified salt.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
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

        /// <summary>
        /// If the search target does not exist or the password does not match, null is returned.
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns></returns>
        public override Domain.Model.User.User Find(Domain.Model.User.User aggregateRoot)
        {
            string sql = $"select id, password, salt from {TableName} where id = @id";
            object param = new { id = aggregateRoot.Id };
            var result = Connection.QueryFirstOrDefault<(Guid id, string password, string salt)>(sql, param, Transaction);
            return result.password == Hash(aggregateRoot.Password.Word, result.salt)
                ? new Domain.Model.User.User(new UserId(result.id), new Password(aggregateRoot.Password.Word))
                : null;
        }

        public override void Update(Domain.Model.User.User aggregateRoot)
        {
            string sql = $"update {TableName} set password = @password where id = @id";
            object param = new { id = aggregateRoot.Id, password = aggregateRoot.Password.Word };
            Connection.Execute(sql, param, Transaction);
        }
    }
}
