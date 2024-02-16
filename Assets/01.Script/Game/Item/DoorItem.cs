using System;
using System.Collections;
using System.Collections.Generic;
using _01.Script.Game.Gate.Event;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoorItem : MonoBehaviour
{

    public Sprite doorSprite;

    public DoorData DoorData;

    public int successWeight;

    public void init(DoorData doorData)
    {
        this.DoorData = doorData;
        successWeight = doorData.ability == 1 ? 100 : 0;
        doorSprite = DoorData.EntitySprite;
        
        if (doorData.Doorname == "None")
            GetComponent<Image>().color = Color.clear;
        else
            GetComponent<Image>().color = Color.white;
    }

    public void StartMove()
    {
        
    }
    
    public void Use(BaseGate gate)
    {
        gate.GateEvent = GateEvents.EmptyGateEvent;
        Sprite open = DoorData.OpenSprite;
        Sprite close = DoorData.CloseSprite;
        gate.openSprite = open;
        gate.closeSprite = close;
        gate.init();
    }
}
