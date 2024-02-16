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
        
        
        if (doorData.Doorname == "None")
            GetComponent<Image>().color = Color.clear;
        else
        {
            GetComponent<Image>().sprite = DoorData.EntitySprite;
            GetComponent<Image>().color = Color.white;
        }
    }

    public void StartMove()
    {
        
    }
    
    public void Use(BaseGate gate)
    {
        if (DoorData.index == 0)
            return;
        gate.GateEvent = GateEvents.EmptyGateEvent;
        Sprite open = DoorData.OpenSprite;
        Sprite close = DoorData.CloseSprite;
        gate.openSprite = open;
        gate.closeSprite = close;
        gate.GetComponent<SpriteRenderer>().sprite = close;
        Debug.Log(close.name);

    }
}
