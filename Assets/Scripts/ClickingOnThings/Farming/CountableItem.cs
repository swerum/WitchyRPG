using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class CountableItem 
{
    public Item item;
    public int number = 1;

    public CountableItem(Item item, int num)
    {
        this.item = item;
        number = num;
    }
}
