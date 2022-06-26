using System;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.RPG.Domain.Model.Scene;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.InMemoryRepository.Scene
{
    public class InMemoryLocationRepository : InMemoryRepository<LocationId, Location>, ILocationRepository
    {
        public override LocationId NextIdentity() => new LocationId(Guid.NewGuid().ToString("N"));
    }
}

