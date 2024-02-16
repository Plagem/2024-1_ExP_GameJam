
using System;
using System.Collections.Generic;
using _01.Script;
using _01.Script.Game.Gate.Event;
using UnityEngine;

public class FloorManager : MonoBehaviour
{    
    private int currentFloor = 0;
    public int CurrentFloor => currentFloor;
    [Header("EmptyGateEvent, MonsterGateEvent, DoorGateEvent, FailGateEvent")]
    public List<int> Probabliteis;
    public List<BaseGate> Gates;

    public void Start()
    {
        
    }

    public void GoFloor(int floor)
    {
        currentFloor = floor;
        initializeFloor();
    }


    public void initializeFloor()
    {
        for (int i = 0; i < 3; i++)
        {
            int eventIdx = Utility.WeightedRandom(Probabliteis.ToArray());
            Gates[i].GateEvent = GateEvents.GateEventList[eventIdx];
            Debug.Log($"init {i} gate : {GateEvents.GateEventList[eventIdx].name}");
        }
    }

    public void GoNextFloor()=>GoFloor(currentFloor+1);


    public void Focus(BaseGate gate)
    {
        foreach (var _gate in Gates)
        {
            _gate.isFocused = false;
        }

        gate.isFocused = true;
    }
}
