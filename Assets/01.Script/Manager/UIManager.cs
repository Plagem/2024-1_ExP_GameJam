
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IFloorChangeListener
{
    [SerializeField]
    private TextMeshProUGUI tmpFloor;
    [SerializeField] public Slider monsterSlider;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject GameoverPrefab;
    public GameObject warningTab;
    public Inventory inventory;

    public UiHoverListener UiHoverListener;
    public bool isGameClickDisabled = false;
    
    public void Start()
    {
        UiHoverListener = _canvas.GetComponent<UiHoverListener>();
    }

    public void OnFloorChanged(int floor)
    {
        tmpFloor.text = floor + "F";
    }

    public void ShowGameOver()
    {
        GameObject gameover = Instantiate(GameoverPrefab,_canvas.transform);
        isGameClickDisabled = true;
    }
}
