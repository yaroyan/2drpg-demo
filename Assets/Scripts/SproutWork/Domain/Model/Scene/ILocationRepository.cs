using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    public interface ILocationRepository : IRepository<LocationId, Location>
    {

    }
}
