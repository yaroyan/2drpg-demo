using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using Yaroyan.SproutWork.Domain.Model.Scene;
using Yaroyan.SproutWork.Domain.Model.User;
using Yaroyan.SproutWork.Infrastructure.DataSource.Repository.InMemoryRepository.Scene;
using Yaroyan.SproutWork.Infrastructure.DataSource.Repository.InMemoryRepository.User;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Repository.InMemoryRepository
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
        public void Rollback() { }
        public void Dispose() { }
    }
}
