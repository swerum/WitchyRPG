using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Game design")]
    [Tooltip("The items in your inventory")]
    [SerializeField] List<Item> items = new List<Item>();
    [Tooltip("The lower this number, the less you have to scroll to change the item you're using.")]
    [SerializeField] float mouseScrollSensitivity = 0.3f;

    [Header("References")]
    [SerializeField] List<GameObject> slots = new List<GameObject>();
    [SerializeField] GameObject selectionSprite = null;
    [SerializeField] MouseClickHandler mouseClickHandler = null;
    int currentSelection = 0;

    private void Start()
    {
        UpdateAllSprites();
        ChangeSelection(0);
    }

    private void Update()
    {
        float mouseScroll = Input.mouseScrollDelta.y;
        if      (mouseScroll <= -mouseScrollSensitivity) { ChangeSelection(currentSelection + 1); }
        else if (mouseScroll >=  mouseScrollSensitivity) { ChangeSelection(currentSelection - 1); }
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

    //public void ReduceInInventory(Item item, int reduceNum)
    //{

    //    if (items.Contains(item))
    //    {
    //        //add another item to list
    //        int index = items.BinarySearch(item);
    //        items[index].numItem += reduceNum;
    //        UpdateSprites(index);
    //    }
    //}

    public void ReduceCurrentItem(int reduceNum)
    {
        if (currentSelection >= items.Count || items[currentSelection] == null) return;
        int newNumItem = items[currentSelection].numItem - reduceNum;
        if (newNumItem <= 0)
        {
            items.RemoveAt(currentSelection);
            UpdateAllSprites();
            ChangeSelection(currentSelection - 1);
        }
        else {
            items[currentSelection].numItem = newNumItem;
            UpdateSprites(currentSelection);
        }
    }

    public bool ItemTypeInInventory(ItemAction itemAction)
    {
        return true;
    }
    #endregion

    #region private methods
    private void UpdateSprites(int index)
    {
        Item item = items[index];
        if (item == null) return;
        GameObject slot = slots[index];
        slot.SetActive(true);
        slot.GetComponent<Image>().sprite = item.sprite;
        slot.transform.GetChild(0).GetComponent<Text>().text = "" + item.numItem;
    }

    private void UpdateAllSprites()
    {
        for (int i = 0; i < items.Count; i++) { UpdateSprites(i); }
        for (int i = items.Count; i < slots.Count; i++) { slots[i].SetActive(false); }
    }

    [ContextMenu("ChangeSelection")]
    private void ChangeSelection(int index)
    {
        //circle around to start/end of inventory
        if (index >= slots.Count) index = 0;
        else if (index < 0) index = slots.Count - 1;
        //change selection
        currentSelection = index;
        selectionSprite.transform.position = slots[currentSelection].transform.position;
        //change item function
        if (currentSelection >= items.Count || items[currentSelection] == null) { mouseClickHandler.ClickAction = ItemAction.Nothing; }
        else {
            mouseClickHandler.ClickAction = items[currentSelection].itemAction;
            mouseClickHandler.Plant = items[currentSelection].plant;
        }
    }
    #endregion
}
