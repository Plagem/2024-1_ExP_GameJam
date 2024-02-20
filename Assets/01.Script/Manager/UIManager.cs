
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
    [SerializeField] public Slider monsterTimer;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject GameoverPrefab;
    public GameObject warningTab;
    public Inventory inventory;
    public GameObject pauseKey;

    public UiHoverListener UiHoverListener;
    public bool isGameClickDisabled = false;

    public GameObject RareImage;
    
    public void Start()
    {
        UiHoverListener = _canvas.GetComponent<UiHoverListener>();
    }

    public void OnFloorChanged(int floor)
    {
        tmpFloor.text = floor + " F";
    }

    public string overMsg = "Error";

    public void ShowGameOver()
    {

        DoorItem item = inventory.GetRare();
        SoundManager.Instance.StopTick();

        monsterSlider.gameObject.SetActive(false);
        warningTab.gameObject.SetActive(false);
        monsterTimer.gameObject.SetActive(false);
        pauseKey.SetActive(false);

        /// 게임오버다 임마
        if (item==null)
        {
            isGameClickDisabled = true;
            GameObject gameover = Instantiate(GameoverPrefab,_canvas.transform);
            gameover.GetComponent<GameoverPopup>().description.text = overMsg;
            SoundManager.Instance.Play("5. game_over");
            return;
        }
        // 이걸 살아?

        var obj = Instantiate(RareImage,_canvas.transform);
        obj.transform.position = item.transform.position;
        isGameClickDisabled = true;
        var rare = obj.GetComponent<RareItem>();
        rare.item = item;
        rare.inventory = inventory;
        GameManager.Instance.FloorManager.ClearEntity();
        
        // GameObject gameover = Instantiate(GameoverPrefab,_canvas.transform);
       
    }


    public void ShowGameOverF()
    {
        isGameClickDisabled = true;
        GameObject gameover = Instantiate(GameoverPrefab,_canvas.transform);
        SoundManager.Instance.Play("5. game_over");
        
    }
}
