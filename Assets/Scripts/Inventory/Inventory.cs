using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Parent Variables")]
    [Tooltip("The items in your inventory")]
    [SerializeField] protected List<Item> items = new List<Item>();
    [SerializeField] protected List<GameObject> slots = new List<GameObject>();

    private void Start()
    {
        UpdateAllSprites();
    }

    #region public methods
    /// <summary>
    /// Add an item to the inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns>returns true if it was successfully added, false, if the inventory was too full</returns>
    public bool AddToInventory(Item item, int num)
    {
        if (items.Contains(item))
        {
            //add another item to list
            int index = items.BinarySearch(item);
            items[index].numItem += num;
            UpdateSprites(index);
        }
        else
        {
            if (items.Count == slots.Count) return false;
            items.Add(item);
            items[items.Count - 1].numItem = num;
            UpdateSprites(items.Count - 1);
        }
        return true;
    }
    public bool AddToInventory(Item item) { return AddToInventory(item, 1); }

    public bool IsFull() { return (items.Count >= slots.Count); }
    public int RoomLeft() { return (slots.Count - items.Count); }

    /// <summary>
    /// Reduces the number in inventory at index by reduceNum
    /// </summary>
    /// <param name="index"></param>
    /// <param name="reduceNum"></param>
    /// <returns>Returns true if the item at index was removed</returns>
    public bool ReduceItemAt(int index, int reduceNum)
    {
        if (index >= items.Count || items[index] == null) return false;
        int newNumItem = items[index].numItem - reduceNum;
        if (newNumItem <= 0)
        {
            items.RemoveAt(index);
            UpdateAllSprites();
            return true;
        }
        else
        {
            items[index].numItem = newNumItem;
            UpdateSprites(index);
            return false;
        }
    }

    public bool ReduceItem(Item item, int reduceNum)
    {
        int index = GetItemIndex(item);
        if (index == -1) return false;
        return ReduceItemAt(index, reduceNum);
    }

    public int GetFirstIndexOfElementOfType(ItemAction itemAction)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemAction == itemAction) return i;
        }
        return -1;
    }

    public int GetItemIndex(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Equals(item)) return i;
        }
        return -1;
    }

    public Item GetItemAtIndex(int index)
    {
        if (index >= items.Count) return null;
        return items[index];
    }

    /// <summary>
    /// Check if there is an item with a specified Item Action in the inventory.
    /// </summary>
    /// <param name="itemAction">The Item action we want to find an item with.</param>
    /// <returns>True if such an item exists.</returns>
    public bool ContainsItemType(ItemAction itemAction)
    {
        Item specifiedItem = items.Find(x => x.itemAction == itemAction);
        return (specifiedItem != null);
    }

    public int NumberItemInInventory(Item item)
    {
        Item specifiedItem = items.Find(x => x.Equals(item));
        if (specifiedItem == null) return 0;
        return specifiedItem.numItem;
    }

    public void UpdateSprites(int index)
    {
        Item item = items[index];
        if (item == null) return;
        GameObject slot = slots[index];
        slot.SetActive(true);
        slot.GetComponent<Image>().sprite = item.sprite;
        slot.transform.GetChild(0).GetComponent<Text>().text = "" + item.numItem;
    }

    public void UpdateAllSprites()
    {
        for (int i = 0; i < items.Count; i++) { UpdateSprites(i); }
        for (int i = items.Count; i < slots.Count; i++) { slots[i].SetActive(false); }
    }
    #endregion

}
