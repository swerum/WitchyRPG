using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : Inventory
{
    [Header("Player Inventory specific")]
    [Tooltip("The lower this number, the less you have to scroll to change the item you're using.")]
    [SerializeField] float mouseScrollSensitivity = 0.1f;

    [SerializeField] GameObject selectionSprite = null;
    int currentSelection = 0;

    private void Start()
    {
        UpdateAllSprites();
        ChangeSelection(0);
    }

    private void Update()
    {
        float mouseScroll = Input.mouseScrollDelta.y;
        if (mouseScroll <= -mouseScrollSensitivity) { ChangeSelection(currentSelection + 1); }
        else if (mouseScroll >= mouseScrollSensitivity) { ChangeSelection(currentSelection - 1); }
    }

    #region public methods

    /// <summary>
    /// reduce the currently selection item
    /// </summary>
    /// <param name="reduceNum"></param>
    public void ReduceCurrentItem(int reduceNum)
    {
        if (ReduceItemAt(currentSelection, reduceNum)) ChangeSelection(currentSelection - 1);
    }

    public CountableItem GetCurrentItem() { return GetItemAtIndex(currentSelection); }

    #endregion

    #region private methods
    private void ChangeSelection(int index)
    {
        //circle around to start/end of inventory
        if (index >= slots.Count) index = 0;
        else if (index < 0) index = slots.Count - 1;
        //change selection
        currentSelection = index;
        selectionSprite.transform.position = slots[currentSelection].transform.position;
        //change item function
        if (currentSelection >= items.Count || items[currentSelection] == null) { MouseClickHandler.Instance.ClickAction = ItemAction.Nothing; }
        else
        {
            MouseClickHandler.Instance.ClickAction = items[currentSelection].item.itemAction;
            MouseClickHandler.Instance.Plant = items[currentSelection].item.plant;
        }
    }
    #endregion

    #region Singleton
    private static PlayerInventory _instance;

    public static PlayerInventory Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion
}
