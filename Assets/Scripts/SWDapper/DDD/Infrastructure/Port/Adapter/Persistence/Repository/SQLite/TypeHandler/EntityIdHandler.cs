using System.Collections;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository.SQLiteRepository.TypeHandler
{
    public abstract class EntityIdHandler<T> : SqlMapper.TypeHandler<T> where T : EntityId
    {
        public override void SetValue(IDbDataParameter parameter, T value)
        {
            parameter.DbType = DbType.Guid;
            parameter.Value = value.Id;
        }
    }
}
