using System;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SproutWork.Domain.Model.Scene;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Repository.InMemoryRepository.Scene
{
    public class InMemoryLocationRepository : InMemoryRepository<LocationId, Location>, ILocationRepository
    {
        public override LocationId NextIdentity() => new LocationId(Guid.NewGuid());
    }
}

