using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exchange : MonoBehaviour
{
    [Header("Price")]
    [SerializeField] int moneyInput = 0;
    [SerializeField] List<Item> itemInput = new List<Item>();

    [Header("Reward")]
    [SerializeField] int moneyOutput = 0;
    [SerializeField] List<Item> itemOutput = new List<Item>();

    /// <summary>
    /// checks if the player has enough money or materials to purchase the goods.
    /// </summary>
    /// <returns>If the player has enough money/items for this purchase.</returns>
    public bool HasInputNeeded()
    {
        //if we don't have enough money
        if (!Wallet.Instance.HasEnoughMoney(moneyInput)) return false;
        foreach (Item item in itemInput)
        {
            if (PlayerInventory.Instance.NumberItemInInventory(item) <= 0) return false;
        }
        return true;
    }

    /// <summary>
    /// Exchanges the input from the players inventory with the output --> buying something
    /// </summary>
    /// <returns>Whether the purchase was successfull</returns>
    public bool ExchangeGoods()
    {
        //if we don't have enough room in our inventory
        if (PlayerInventory.Instance.RoomLeft()-itemInput.Count < itemOutput.Count) return false;
        //if we have the things for it
        if (!HasInputNeeded()) return false;
        //actually exchange goods
        foreach(Item item in itemInput)
        {
            PlayerInventory.Instance.ReduceItem(item, 1);
        }
        foreach(Item item in itemOutput)
        {
            PlayerInventory.Instance.AddToInventory(item);
        }
        Wallet.Instance.SpendMoney(moneyInput);
        Wallet.Instance.AddMoney(moneyOutput);
        return true;
    }
}
