using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectGetMaterial : ClickableItem
{
    [SerializeField] Item itemAddedToInventory = null;
    public override void RightClick()
    {
        if (itemAddedToInventory != null)
        {
            PlayerInventory.Instance.AddToInventory(new CountableItem(itemAddedToInventory, 1));
        }
        Destroy(gameObject);
    }
}
