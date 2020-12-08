using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinInventory : Inventory
{

    public void GiveLastItemBack() { GiveItemBackToPlayer(items.Count - 1); }

    private void GiveItemBackToPlayer(int index)
    {
        if (PlayerInventory.Instance.IsFull()) return;
        if (index >= items.Count || index < 0) return;
        int num = items[index].numItem;
        PlayerInventory.Instance.AddToInventory(items[index], num);
        ReduceItemAt(index, num);
    }
}
