using System;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SproutWork.Domain.Model.Scene;
using Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Repository.InMemoryRepository.Scene
{
    public class InMemoryRouteRepository : InMemoryRepository<RouteId, Route>, IRouteRepository
    {
        public override RouteId NextIdentity() => new RouteId(Guid.NewGuid());
    }
}
