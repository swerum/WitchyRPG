using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectGetMaterial : ClickableItem
{
    [SerializeField] Item itemAddedToInventory = null;

    /// <summary>
    /// Destroys the Clickable object and adds a specified item to the player's Inventory.
    /// </summary>
    public override void RightClick()
    {
        if (itemAddedToInventory != null)
        {
            PlayerInventory.Instance.AddToInventory(new CountableItem(itemAddedToInventory, 1));
        }
        Destroy(gameObject);
    }
}
