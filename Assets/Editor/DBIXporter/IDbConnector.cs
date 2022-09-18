using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;


namespace Yaroyan.SproutWork.EditorExtension.IXporter
{
    public interface IDbConnector
    {
        IDbConnection ProvideConnection();
    }
}

