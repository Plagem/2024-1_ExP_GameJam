using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class DoorUI : MonoBehaviour, IPointerClickHandler
{
    [FormerlySerializedAs("door")] public BaseGate gate;
    
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
        if(gate)
            gate.OnOpen();
    }
}
