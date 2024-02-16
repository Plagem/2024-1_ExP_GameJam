using System.Collections;
using System.Collections.Generic;
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
        doorSprite = Resources.LoadAll<Sprite>("image/Doors/testsheet")[0];
        
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
        
    }
}
