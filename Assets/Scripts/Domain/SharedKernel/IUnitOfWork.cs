using System;
using System.Threading;
using System.Threading.Tasks;
using Yaroyan.Game.RPG.Domain.Model.Scene;

namespace Yaroyan.Game.DDD.SharedKernel
{
    public interface IUnitOfWork : IDisposable
    {
        ISceneRepository SceneRepository { get; }
        ILocationRepository LocationRepository { get; }
        IRouteRepository RouteRepository { get; }
        void Commit();
    }
}