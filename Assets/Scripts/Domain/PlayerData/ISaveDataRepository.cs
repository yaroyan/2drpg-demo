using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;

namespace Yaroyan.Game.RPG.Domain.Model.SaveData
{
    public interface ISaveDataRepository : IRepository<SaveDataId, SaveData>
    {

    }
}
