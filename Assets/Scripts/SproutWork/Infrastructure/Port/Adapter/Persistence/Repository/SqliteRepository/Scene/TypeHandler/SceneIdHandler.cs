using System.Collections;
using System.Collections.Generic;
using Yaroyan.SproutWork.Domain.Model.Scene;
using System;
using Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository.SQLiteRepository.TypeHandler;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.TypeHandler
{
    public class SceneIdHandler : EntityIdHandler<SceneId>
    {
        public override SceneId Parse(object value) => value is System.DBNull ? null : new SceneId(Guid.Parse((string)value));
    }
}
