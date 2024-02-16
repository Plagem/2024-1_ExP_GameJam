
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour, IFloorChangeListener
{
    [SerializeField]
    private TextMeshProUGUI tmpFloor;

    public bool isGameClickDisabled = false;
    
    public void Start()
    {
        
    }

    public void OnFloorChanged(int floor)
    {
        tmpFloor.text = floor + "F";
    }
    
    
}
