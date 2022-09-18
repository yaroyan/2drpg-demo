using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.RPG.Domain.Model.Scene;
using Yaroyan.Game.RPG.Domain.Model.User;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource
{
    public interface IUnitOfWork : SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository.IUnitOfWork
    {
        ISceneRepository SceneRepository { get; }
        ILocationRepository LocationRepository { get; }
        IRouteRepository RouteRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
