using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Parent Variables")]
    [Tooltip("The items in your inventory")]
    [SerializeField] protected List<CountableItem> items = new List<CountableItem>();
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
    public bool AddToInventory(CountableItem countableItem)
    {
        Item item = countableItem.item;
        int num = countableItem.number;
        int index = GetItemIndex(item);
        if (index != -1)
        {
            //add another item to list
            items[index].number += num;
            UpdateSprites(index);
        }
        else
        {
            if (items.Count == slots.Count) return false;
            CountableItem newCountable = new CountableItem(item, num);
            items.Add(newCountable);
            items[items.Count - 1].number = num;
            UpdateSprites(items.Count - 1);
        }
        return true;
    }
    public bool AddToInventory(Item item)
    {
        CountableItem countableItem = new CountableItem(item, 1);
        return AddToInventory(countableItem);
    }

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
        int newNumItem = items[index].number- reduceNum;
        if (newNumItem <= 0)
        {
            items.RemoveAt(index);
            UpdateAllSprites();
            return true;
        }
        else
        {
            items[index].number = newNumItem;
            UpdateSprites(index);
            return false;
        }
    }

    public bool ReduceItem(CountableItem item)
    {
        int index = GetItemIndex(item.item);
        if (index == -1) return false;
        return ReduceItemAt(index, item.number);
    }

    public int GetFirstIndexOfElementOfType(ItemAction itemAction)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item.itemAction == itemAction) return i;
        }
        return -1;
    }

    public int GetItemIndex(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item.Equals(item)) return i;
        }
        return -1;
    }

    public CountableItem GetItemAtIndex(int index)
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
        CountableItem specifiedItem = items.Find(x => x.item.itemAction == itemAction);
        return (specifiedItem != null);
    }

    public bool ContainsItem(Item item)
    {
        CountableItem specifiedItem = items.Find(x => x.item == item);
        return (specifiedItem != null);
    }

    public int NumberItemInInventory(CountableItem item)
    {
        CountableItem specifiedItem = items.Find(x => x.Equals(item));
        if (specifiedItem == null) return 0;
        return specifiedItem.number;
    }

    public void UpdateSprites(int index)
    {
        CountableItem item = items[index];
        if (item == null) return;
        GameObject slot = slots[index];
        slot.SetActive(true);
        slot.GetComponent<Image>().sprite = item.item.sprite;
        slot.transform.GetChild(0).GetComponent<Text>().text = "" + item.number;
    }

    public void UpdateAllSprites()
    {
        for (int i = 0; i < items.Count; i++) { UpdateSprites(i); }
        for (int i = items.Count; i < slots.Count; i++) { slots[i].SetActive(false); }
    }
    #endregion

}
