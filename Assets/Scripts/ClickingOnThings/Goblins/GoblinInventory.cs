using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinInventory : Inventory
{
    /// <summary>
    /// Give back the last item that was given to you i.e. The item at the end of your inventory
    /// </summary>
    public void GiveLastItemBack() { GiveItemBackToPlayer(items.Count - 1); }

    /// <summary>
    /// Give the item at index back to the player
    /// </summary>
    /// <param name="index"></param>
    private void GiveItemBackToPlayer(int index)
    {
        if (PlayerInventory.Instance.IsFull()) return;
        if (index >= items.Count || index < 0) return;
        int num = items[index].number;
        PlayerInventory.Instance.AddToInventory(items[index]);
        ReduceItemAt(index, num);
    }
}
