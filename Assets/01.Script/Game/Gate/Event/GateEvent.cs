
using System;

public class GateEvent
{
    public static GateEvent EmptyGateEvent = new GateEvent();
    public static GateEvent MonsterGateEvent = new GateEvent();
    public static GateEvent DoorGateEvent = new GateEvent();
    public static GateEvent FailGateEvent = new GateEvent();


    private Action OnOpen;

    private GateEvent SetOpenEvent(Action action)
    {
        this.OnOpen = action;
        return this;
    }
}
