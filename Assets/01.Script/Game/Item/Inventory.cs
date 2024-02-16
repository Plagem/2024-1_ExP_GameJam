using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Item
{
    Item1,
    Item2,
    Item3,
    Item4,
    Item5,
    None
}

public class Inventory : MonoBehaviour
{
    private int maxItemCount;
    private int itemCount;

    [SerializeField]
    private GameObject[] ItemSlot;

    [SerializeField]
    private GameObject[] ItemPrefebs;
    
    public Item[] CharacterItems;


    private void Start()
    {
        InitInventory();
    }

    // ���� ����, ���� ����, ���� ����� �� �κ��丮�� �ʱ�ȭ�Ѵ�
    public void InitInventory()
    {
        Debug.Log("Init Inventory");
        maxItemCount = 5;
        itemCount = 0;
        CharacterItems = Enumerable.Repeat(Item.None, maxItemCount).ToArray();
    }

    // �������� ��´�
    private void GetItem(Item item)
    {
        Debug.Log("Get Item");
        Debug.Log(itemCount);
        if(itemCount < maxItemCount)
        {
            for(int i = 0; i < maxItemCount; i++)
            {
                if (CharacterItems[i] == Item.None)
                {
                    CharacterItems[i] = item;
                    itemCount++;
                    break;
                }
            }

            DrawInventory();
        }
        // �κ��丮�� �� á�� ���
        else
        {
            Debug.Log("Full Inventory - Can't Get Item");
            return;
        }
    }

    private void DrawInventory()
    {
        // ������ ĭ ����ŭ For Loop
        for(int i = 0; i < maxItemCount;i++)
        {
            // ���� ������ �̹��� ����
            for(int j = 0; j < ItemSlot[i].transform.childCount; j++)
            {
                Destroy(ItemSlot[i].transform.GetChild(0).gameObject);
            }
            // ���ο� ������ �̹��� ����
            int nowItemNum = (int)CharacterItems[i];
            GameObject nowDrawingItem = Instantiate(ItemPrefebs[nowItemNum]);
            nowDrawingItem.transform.SetParent(ItemSlot[i].transform);
            nowDrawingItem.transform.localPosition = Vector3.zero;
        }
    }

    public void UsingItem(int itemSlot)
    {
        CharacterItems[itemSlot] = Item.None;
        itemCount--;
        DrawInventory();
    }


    public void GetItemTest()
    {
        GetItem(0);
    }

    public void UsingItemTest()
    {
        UsingItem(2);
    }
}
