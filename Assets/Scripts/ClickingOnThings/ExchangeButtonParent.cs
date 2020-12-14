using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeButtonParent : MonoBehaviour
{
    Exchange[] exchangeButtons;
    void Start()
    {
        exchangeButtons = GetComponentsInChildren<Exchange>();
    }

    /// <summary>
    /// Function is called when the player spends or makes money. It updates the buttons to show if the player can buy this item or not.
    /// </summary>
    public void UpdateButtons()
    {
        foreach (Exchange e in exchangeButtons) { e.UpdateImage(); }
    }

}
