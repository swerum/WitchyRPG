using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public float price = 5f;
    public ItemAction itemAction;
    public int numItem = 1;
    public Sprite sprite;
    [Header("If this is a seed, add a Plant Info")]
    public PlantInfo plant;
    [Header("If it is a placable Item, add a prefab")]
    public GameObject placableObjectPrefab;
}
