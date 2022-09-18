using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource
{
    public interface ILiteDBConfig : IDbConfig
    {
        /// <summary>
        /// Provide connection string.
        /// </summary>
        /// <returns></returns>
        string getConnectionString();
    }
}
