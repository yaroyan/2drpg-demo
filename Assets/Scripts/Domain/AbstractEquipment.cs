using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractEquipment : AbstractItem, IEquipable
{
    public void Equip()
    {
        throw new System.NotImplementedException();
    }
}
