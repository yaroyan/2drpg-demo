using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleService : MonoBehaviour
{
    private IEnumerable<IUpdatable> updatables;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (IUpdatable updatable in updatables) updatable.ManagedUpdate();
    }

    private List<IUpdatable> SortExecutionOrder()
    {
        return default;
    }
}
