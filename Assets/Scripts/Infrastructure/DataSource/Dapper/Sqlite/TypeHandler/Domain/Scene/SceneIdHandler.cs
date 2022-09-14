using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.RPG.Domain.Model.Scene;
using System;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Dapper.TypeHandler.Domain.Model.Scene
{
    public class SceneIdHandler : EntityIdHandler<SceneId>
    {
        public override SceneId Parse(object value) => value is System.DBNull ? null : new SceneId(Guid.Parse((string)value));
    }
}
