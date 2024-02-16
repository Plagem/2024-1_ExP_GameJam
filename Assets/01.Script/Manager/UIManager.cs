
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour, IFloorChangeListener
{
    [SerializeField]
    private TextMeshProUGUI tmpFloor;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject GameoverPrefab;

    public bool isGameClickDisabled = false;
    
    public void Start()
    {
        
    }

    public void OnFloorChanged(int floor)
    {
        tmpFloor.text = floor + "F";
    }

    public void ShowGameOver()
    {
        GameObject gameover = Instantiate(GameoverPrefab,_canvas.transform);
        
    }
}
