using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
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
        if (drag.isDragging)
        {
            foreach (var item in Items)
            {
                item.transform.Translate(0, drag.deltaY, 0, Space.World);
            }
        }
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
        foreach (var item in Items)
        {
            item.init(None);
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

    public void UsingItem(int itemSlot, BaseGate gate)
    {
        DoorItem item = Items[itemSlot];
        if (item.DoorData == None)
            return;
        item.Use(gate);
        item.init(None);
        itemCount--;
    }


    public void AddItemTest()
    {
        AddItem(GameManager.Instance.FloorManager.AllDoorDataList[Random.Range(1,11)]);
    }
    
    public void UsingItemTest()
    {
        UsingItem(2,gates[2]);
    }
}
