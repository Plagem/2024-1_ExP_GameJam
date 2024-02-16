
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoorUI : BaseUI
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI floorInfo;
    public Button left;
    public Button right;


    public int idx = 0;
    
    private void OnEnable()
    {
       
    }
    

    public void OnUse()
    {
        GameManager.Instance.IngameUIManager.inventory.UseItemRight(idx);
        Destroy(this);
    }
    
    public void OnCancel()
    {
        Destroy(this);
    }
}
