using System.Collections;
using System.Collections.Generic;
using Yaroyan.SproutWork.Domain.Model.Scene;
using System;
using Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository.SQLiteRepository.Dapper.TypeHandler;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Dapper.TypeHandler.Domain.Model.Scene
{
    public class SceneIdHandler : EntityIdHandler<SceneId>
    {
        public override SceneId Parse(object value) => value is System.DBNull ? null : new SceneId(Guid.Parse((string)value));
    }
}
