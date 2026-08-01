using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerHub : MonoBehaviour
{
    protected Player player;

    public bool Initialized { get; private set; }

    public void Init(Player owner)
    {
        player = owner;
        OnInit();
        Initialized = true;
    }

    protected virtual void OnInit() { }
}
