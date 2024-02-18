using System;
using System.Collections;
using System.Collections.Generic;
using _01.Script.Game.Gate.Event;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DoorItem : MonoBehaviour
{

    public Sprite doorSprite;

    public DoorData DoorData;

    public int successWeight;

    public float FixedHeight = 128f;
    
    
    
    public void init(DoorData doorData)
    {
        this.DoorData = doorData;
        successWeight = doorData.ability == 1 ? 100 : 0;

        Image img = GetComponent<Image>();
        
        if (doorData.Doorname == "None")
            img.color = Color.clear;
        else
        {
            img.sprite = DoorData.EntitySprite;
            img.color = Color.white;
            // 사이즈 재설정
            Vector2 orinalSize = DoorData.EntitySprite.texture.Size();
            float ratio = orinalSize.x / orinalSize.y;
            
            GetComponent<RectTransform>().sizeDelta = new Vector2(ratio * FixedHeight,FixedHeight);
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
        gate.State = BaseGate.GateState.Close;
        SoundManager.Instance.Play("15. inven_door_use");
        
    }
}
