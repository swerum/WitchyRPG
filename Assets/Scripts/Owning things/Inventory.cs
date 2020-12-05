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
    public void AddToInventory(Item item)
    {
        if (items.Contains(item))
        {
            //add another item to list
            int index = items.BinarySearch(item);
            items[index].numItem += item.numItem;
            UpdateSprites(index);
        }
        else
        {
            items.Add(item);
            UpdateSprites(items.Count - 1);
        }
    }

    public bool ReduceItem(int index, int reduceNum)
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
    #endregion

    #region private methods
    protected void UpdateSprites(int index)
    {
        Item item = items[index];
        if (item == null) return;
        GameObject slot = slots[index];
        slot.SetActive(true);
        slot.GetComponent<Image>().sprite = item.sprite;
        slot.transform.GetChild(0).GetComponent<Text>().text = "" + item.numItem;
    }

    protected void UpdateAllSprites()
    {
        for (int i = 0; i < items.Count; i++) { UpdateSprites(i); }
        for (int i = items.Count; i < slots.Count; i++) { slots[i].SetActive(false); }
    }
    #endregion

}
