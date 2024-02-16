using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Item
{
    None,
    Item1,
    Item2,
    Item3,
    Item4,
    Item5
}

public class Inventory : MonoBehaviour
{
    private int maxItemCount;
    private int itemCount;
    public Item[] characterItems;


    // ���� ����, ���� ����, ���� ����� �� �κ��丮�� �ʱ�ȭ�Ѵ�
    void InitInventory()
    {
        Debug.Log("Init Inventory");
        maxItemCount = 5;
        itemCount = 0;
        characterItems = Enumerable.Repeat(Item.None, maxItemCount).ToArray();
    }

    // �������� ��´�
    void GetItem(Item item)
    {
        // �κ��丮�� �� á�� ���
        if(itemCount >= maxItemCount - 1)
        {
            characterItems[itemCount] = item;
            itemCount++;
        }
        {
            Debug.Log("Full Inventory - Can't Get Item");
            return;
        }
    }

}
