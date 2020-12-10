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

    public void UpdateButtons()
    {
        foreach (Exchange e in exchangeButtons) { e.UpdateImage(); }
    }

}
