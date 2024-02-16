using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorUI : MonoBehaviour, IPointerClickHandler
{
    public BaseDoor door;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(door)
            door.OnOpen();
    }
}
