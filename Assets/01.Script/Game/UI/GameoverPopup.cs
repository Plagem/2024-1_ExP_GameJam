
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameoverPopup : BaseUI
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI floorInfo;
    public Button left;
    public Button right;

    private void OnEnable()
    {
        floorInfo.text = $"현재위치 : 지하 {GameManager.Instance.FloorManager.CurrentFloor}층";
    }
    

    public void OnHome()
    {
        GameManager.Instance.LoadHome();
    }
    
    public void OnRestart(){
        GameManager.Instance.Restart();
    }
}
