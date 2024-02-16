
using System;
using UnityEngine.Events;

[Serializable]
public class GateEvent
{
    
    public Action<BaseGate> OnOpen;
    public string name;

    public GateEvent(string name)
    {
        this.name = name;
    }

    public GateEvent SetOpenEvent(Action<BaseGate>  action)
    {
        this.OnOpen = action;
        return this;
    }
}
