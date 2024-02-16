using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGate : MonoBehaviour
{
    public FloorManager fm;
    public GateEvent GateEvent;
    public bool isFocused;
    
    
    // Start is called before the first frame update
    void Start()
    {
        fm = GameManager.Instance.FloorManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnOpen()
    {
        if (GateEvent == null)
        {
            Debug.LogError("이벤트 지정되지 않음");
            return;
        }
        GateEvent.OnOpen.Invoke(this);
    }

    public void OnMouseDown()
    {
        if (!isFocused)
        {
            fm.Focus(this);
            return;
        }
        OnOpen();
    }

    public void Summon(MonsterBase monsterBase)
    {
        
    }
}
