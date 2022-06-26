using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Com.Github.Yaroyan.Rpg.CQRS;

public class CQRSTester : MonoBehaviour
{
    ICommandBus commandBus;
    [Inject]
    void Inject(ICommandBus commandBus)
    {
        this.commandBus = commandBus;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
}
