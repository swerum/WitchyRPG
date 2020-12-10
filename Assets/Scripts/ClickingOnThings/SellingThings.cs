﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingThings : ClickableItem
{
    [Tooltip("All the things you can sell here and the price you'll get for them.")]
    [SerializeField] List<CountableItem> sellableItems = new List<CountableItem>();

    public override void LeftClick()
    {
        int price = GetPrice(PlayerInventory.Instance.GetCurrentItem().item);
        if (price != -1) {
            Wallet.Instance.AddMoney(price);
            PlayerInventory.Instance.ReduceCurrentItem(1);
        }
    }

    private int GetPrice(Item item)
    {
        foreach(CountableItem countableItem in sellableItems)
        {
            if (countableItem.item.Equals(item))
            {
                return countableItem.number;
            }
        }
        return -1;
    }
}
