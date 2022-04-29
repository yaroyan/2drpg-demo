using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractChest : MonoBehaviour, IChest
{
    public void Close()
    {
        Debug.Log("Closed");
    }

    public void Lock()
    {
        Debug.Log("Locked");
    }

    public void Open()
    {
        Debug.Log("Opened");
    }

    public void Unlock()
    {
        Debug.Log("Unlocked");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
