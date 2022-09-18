using System.Collections;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Dapper.TypeHandler
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
