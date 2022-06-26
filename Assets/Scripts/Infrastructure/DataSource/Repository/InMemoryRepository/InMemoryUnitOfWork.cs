using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;
using Yaroyan.Game.RPG.Domain.Model.Scene;
using Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.InMemoryRepository.Scene;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.InMemoryRepository
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        ILocationRepository _locationRepository;
        ISceneRepository _sceneRepository;
        IRouteRepository _routeRepository;
        public ISceneRepository SceneRepository => _sceneRepository ??= new InMemorySceneRepository();
        public ILocationRepository LocationRepository => _locationRepository ??= new InMemoryLocationRepository();
        public IRouteRepository RouteRepository => _routeRepository ??= new InMemoryRouteRepository();
        public void Commit() { }
        public void Dispose() { }
    }
}
