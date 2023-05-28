using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SproutWork.Infrastructure.DataSource
{
    public class InMemoryLiteDBConfig : ILiteDBConfig
    {
        public string ConnectionString { get => throw new System.NotImplementedException(); init => throw new System.NotImplementedException(); }
    }
}
