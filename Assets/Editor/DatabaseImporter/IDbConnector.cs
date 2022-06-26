using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;


namespace Yaroyan.Game.EditorExtension.Importer
{
    public interface IDbConnector
    {
        IDbConnection ProvideConnection();
    }
}

