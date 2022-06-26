using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaroyan.Game.DDD.SharedKernel;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    public interface ILocationRepository : IRepository<LocationId, Location>
    {

    }
}
