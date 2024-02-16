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


    // 게임 시작, 게임 오버, 게임 재시작 시 인벤토리를 초기화한다
    void InitInventory()
    {
        Debug.Log("Init Inventory");
        maxItemCount = 5;
        itemCount = 0;
        characterItems = Enumerable.Repeat(Item.None, maxItemCount).ToArray();
    }

    // 아이템을 얻는다
    void GetItem(Item item)
    {
        // 인벤토리가 꽉 찼을 경우
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
