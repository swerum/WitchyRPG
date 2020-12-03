using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<GameObject> slots = new List<GameObject>();
    List<Item> items = new List<Item>();

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

    private void UpdateSprites(int index)
    {
        Item item = items[index];
        GameObject slot = slots[index];
        slot.SetActive(true);
        slot.GetComponent<Image>().sprite = item.sprite;
        slot.transform.GetChild(0).GetComponent<Text>().text = "" + item.numItem;
    }
}
