using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exchange : MonoBehaviour
{
    [Header("Price")]
    [SerializeField] int moneyInput = 0;
    [SerializeField] List<CountableItem> itemInput = new List<CountableItem>();

    [Header("Reward")]
    [SerializeField] int moneyOutput = 0;
    [SerializeField] List<CountableItem> itemOutput = new List<CountableItem>();

    [Header("Child Reference")]
    [SerializeField] Image darkCover = null;

    ExchangeButtonParent parent;
    private void Start()
    {
        parent = GetComponentInParent<ExchangeButtonParent>();
        UpdateImage();
    }

    /// <summary>
    /// Exchanges the input from the players inventory with the output --> buying something
    /// </summary>
    /// <returns>Whether the purchase was successfull</returns>
    public void ExchangeGoods()
    {
        //if we don't have enough room in our inventory
        if (PlayerInventory.Instance.RoomLeft()-itemInput.Count < itemOutput.Count) return;
        //if we have the things for it
        if (!HasInputNeeded()) return;
        //actually exchange goods
        foreach (CountableItem item in itemInput)
        {
            PlayerInventory.Instance.ReduceItem(item);
        }
        foreach (CountableItem item in itemOutput)
        {
            PlayerInventory.Instance.AddToInventory(item);
        }
        Wallet.Instance.SpendMoney(moneyInput);
        Wallet.Instance.AddMoney(moneyOutput);
        parent.UpdateButtons();
    }

    /// <summary>
    /// Makes image slightly transparent if we cannot buy/make this item
    /// </summary>
    public void UpdateImage()
    {
        if (HasInputNeeded()) darkCover.enabled = false;
        else darkCover.enabled = true;
    }

    /// <summary>
    /// checks if the player has enough money or materials to purchase the goods.
    /// </summary>
    /// <returns>If the player has enough money/items for this purchase.</returns>
    private bool HasInputNeeded()
    {
        //if we don't have enough money
        if (!Wallet.Instance.HasEnoughMoney(moneyInput)) return false;
        foreach (CountableItem item in itemInput)
        {
            if (PlayerInventory.Instance.NumberItemInInventory(item) <= 0) return false;
        }
        return true;
    }
}
