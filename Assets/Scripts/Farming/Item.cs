using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemApplication { None, ProtectFromSound, HealPlayer};

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public float price = 5f;
    public ItemApplication itemApplication;
    public int numItem = 1;
    public Sprite sprite;
}
