using System.Collections;
using System.Collections.Generic;
using Yaroyan.SproutWork.Domain.Model.Scene;
using Yaroyan.SproutWork.Domain.Model.User;

namespace Yaroyan.SproutWork.Infrastructure.DataSource
{
    public interface IUnitOfWork : SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository.IUnitOfWork
    {
        ISceneRepository SceneRepository { get; }
        ILocationRepository LocationRepository { get; }
        IRouteRepository RouteRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
