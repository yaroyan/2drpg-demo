using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SproutWork.Infrastructure.DataSource
{
    public interface ILiteDBConfig : IDbConfig
    {
        /// <summary>
        /// Provide connection string.
        /// </summary>
        /// <returns></returns>
        string ConnectionString { get; init; }
    }
}
