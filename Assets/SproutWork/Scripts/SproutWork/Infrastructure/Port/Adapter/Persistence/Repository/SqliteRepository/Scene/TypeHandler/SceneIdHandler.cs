using System.Collections;
using System.Collections.Generic;
using Yaroyan.SproutWork.Domain.Model.Scene;
using System;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository.SQLiteRepository.TypeHandler;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.TypeHandler
{
    public class SceneIdHandler : EntityIdHandler<SceneId>
    {
        public override SceneId Parse(object value) => value is DBNull ? null : new SceneId((string)value);
    }
}
