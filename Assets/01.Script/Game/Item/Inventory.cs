using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Inventory : MonoBehaviour
{
    private int maxItemCount;
    private int itemCount;

    [SerializeField]
    private GameObject[] ItemSlot;
    private DoorItem[] Items;
    
    [FormerlySerializedAs("ItemPrefAb")] [FormerlySerializedAs("ItemPrefeb")] [SerializeField]
    private GameObject ItemPrefab;

    public DoorData None;
    [SerializeField] private Drag drag;


    [SerializeField] private List<BaseGate> gates;

    private int idx = 0;

    public int Index
    {
        get => idx;
        set
        {
            // 11 이상은 핸들링 못함
            if (idx >= maxItemCount)
                idx = value - maxItemCount;
            else if (idx < 0)
                idx = value + maxItemCount - 1;
            else
                idx = value;
        }
    }


    private void Start()
    {
        InitInventory();
       
    }

    private void Update()
    {
        // if (drag.isDragging)
        // {
        //     foreach (var item in Items)
        //     {
        //         item.transform.Translate(0, drag.deltaY, 0, Space.World);
        //     }
        // }
    }

    /// <summary>
    /// 게임 시작, 게임 오버, 게임 재시작 시 인벤토리를 초기화한다
    /// </summary>
    public void InitInventory()
    {
        Debug.Log("Init Inventory");
        maxItemCount = 5;
        itemCount = 0;
        Items = ItemSlot.Select((obj) => obj.transform.GetChild(0).GetComponent<DoorItem>()).ToArray();
        for (int i = 0; i < maxItemCount; i++)
        {
            Items[i].init(None);
            Items[i].GetComponent<Button>().onClick.AddListener(() => UsingItem(i,gates[2]));
            Items[i].GetComponent<Button>().onClick.AddListener(()=>Debug.Log("sadasd"));
        }
        // CharacterItems = Enumerable.Repeat<>(null, maxItemCount).ToArray();
    }

    /// <summary>
    /// 아이템을 인벤토리에 추가한다.
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(DoorData item)
    {
        Debug.Log("Add Item");
        Debug.Log(itemCount);
        if(itemCount < maxItemCount)
        {
            for(int i = 0; i < maxItemCount; i++)
            {
                Debug.Log(Items[i].DoorData);
                if (Items[i].DoorData == None)
                {
                    Items[i].init(item);
                    itemCount++;
                    break;
                }
            }
        }
        // 인벤토리가 꽉 찼을 경우
        else
        {
            SoundManager.Instance.Play("17. inven_full");
            Debug.Log("Full Inventory - Can't Add Item");
            return;
        }
    }

    private void DrawInventory()
    {
        // 아이템 칸 수만큼 For Loop
        for(int i = 0; i < maxItemCount;i++)
        {
            // // 현재 아이템 이미지 삭제
            // for(int j = 0; j < ItemSlot[i].transform.childCount; j++)
            // {
            //     Destroy(ItemSlot[i].transform.GetChild(0).gameObject);
            // }
            // 새로운 아이템 이미지 삽입
            // int nowItemNum = (int)CharacterItems[i];
            // GameObject nowDrawingItem = Instantiate(ItemPrefebs[nowItemNum]);
            // nowDrawingItem.transform.SetParent(ItemSlot[i].transform);
            // nowDrawingItem.transform.localPosition = Vector3.zero;
        }
    }

    public List<GameObject> HaveToDeactiveList = new List<GameObject>();
    public int SelectedItemIdx = -1;

    /// <summary>
    /// 아이템 선택시 UI
    /// </summary>
    /// <param name="slot"></param>
    public void SelectItemToDoor(int slot){
        DoorItem item = Items[slot];
        if (HaveToDeactiveList.Any() || item.DoorData == None || item.DoorData.isRare)
        {
            return;
        }
        SelectedItemIdx = slot;
        HaveToDeactiveList.Clear();
        var fm = GameManager.Instance.FloorManager;
        GameObject focused = ItemSlot[slot].transform.GetChild(1).gameObject;
        focused.SetActive(true);
        HaveToDeactiveList.Add(focused);
        for (int i = 0; i < fm.Gates.Count; i++)
        {
            BaseGate gate = fm.Gates[i];
            GameObject arrow = gate.gameObject.transform.GetChild(1).gameObject;
            arrow.SetActive(true);
            HaveToDeactiveList.Add(arrow);
        }
    }
    public void UsingItem(int itemSlot, BaseGate gate)
    {
        Debug.Log($"{itemSlot} used");
        DoorItem item = Items[itemSlot];
        if (item.DoorData == None)
            return;
        if (item.DoorData.isRare)
            return; // 레어는 아래에서
        if (!GameManager.Instance.FloorManager.IsGateClickable)
        {
            return;
        }
        item.Use(gate);
        item.init(None);
        itemCount--;
    }

    public DoorItem GetRare()
    {
        for (int i = 0; i < maxItemCount; i++)
        {
            if (Items[i].DoorData.isRare)
            {
                DoorItem item = Items[i];
                itemCount--;
                return item;
            }
        }

        return null;
    }
    


    public void AddItemTest()
    {
        AddItem(GameManager.Instance.FloorManager.AllDoorDataList[12]);
    }
    
    public void UsingItemTest()
    {
        UsingItem(2,gates[2]);
    }
}
