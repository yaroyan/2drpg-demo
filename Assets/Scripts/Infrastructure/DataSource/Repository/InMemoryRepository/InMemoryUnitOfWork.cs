using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;
using Yaroyan.Game.RPG.Domain.Model.Scene;
using Yaroyan.Game.RPG.Domain.Model.User;
using Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.InMemoryRepository.Scene;
using Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.InMemoryRepository.User;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.InMemoryRepository
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        ILocationRepository _locationRepository;
        ISceneRepository _sceneRepository;
        IRouteRepository _routeRepository;
        IUserRepository _userRepository;
        public ISceneRepository SceneRepository => _sceneRepository ??= new InMemorySceneRepository();
        public ILocationRepository LocationRepository => _locationRepository ??= new InMemoryLocationRepository();
        public IRouteRepository RouteRepository => _routeRepository ??= new InMemoryRouteRepository();
        public IUserRepository UserRepository => _userRepository ??= new InMemoryUserRepository();
        public void Commit() { }
        public void Dispose() { }
    }
}
