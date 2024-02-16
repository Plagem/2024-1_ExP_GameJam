using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGate : MonoBehaviour
{

    public GateEvent GateEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnOpen()
    {
        if (GateEvent == null)
        {
            Debug.LogError("이벤드 지정되지 않음");
            return;
        }
        
    }
    public void OnClick()
    {
        
    }
}
