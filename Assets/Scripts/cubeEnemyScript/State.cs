using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{

    public GameObject self { get; private set; }
    public abstract System.Type Tick();
    public State (GameObject self)
    {
        this.self = self;
    }

}
