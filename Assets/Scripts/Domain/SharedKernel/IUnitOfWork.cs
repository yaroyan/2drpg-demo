using System;
using System.Threading;
using System.Threading.Tasks;
using Yaroyan.Game.RPG.Domain.Model.Scene;
using Yaroyan.Game.RPG.Domain.Model.User;

namespace Yaroyan.Game.DDD.SharedKernel
{
    public interface IUnitOfWork : IDisposable
    {
        ISceneRepository SceneRepository { get; }
        ILocationRepository LocationRepository { get; }
        IRouteRepository RouteRepository { get; }
        IUserRepository UserRepository { get; }
        void Commit();
    }
}