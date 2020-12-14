using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    [Tooltip("What is this item called.")]
    public string itemName;
    [Tooltip("What does this item do?")]
    public ItemAction itemAction;
    [Tooltip("How will this item look in your inventory")]
    public Sprite sprite;

    [Header("If this is a seed, add a Plant Info")]
    [Tooltip("What plant will this seed produce.")]
    public PlantInfo plant;

    [Header("If it is a placable Item, add a prefab")]
    [Tooltip("What object will be created if this item is placed.")]
    public GameObject placableObjectPrefab;
}
