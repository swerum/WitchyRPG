using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant Info", menuName = "Plant Info", order = 0)]
public class PlantInfo : ScriptableObject
{
    [Header("The Plant")]
    public string plantName;
    [Tooltip("The Sprite of the seedBag used for planting")]
    public Sprite seedBagSprite = null;
    public Item harvest = null;
    //the plant you get from this

    [Header("The Growth Process")]
    [Tooltip("The number of days needed to get from one stage of growth to the next. Length must be one less thanthe Plant Sprites Array Length.")]
    public int[] days;
    [Tooltip("An array of sprites showing the plant at its different stages of growth.")]
    public Sprite[] plantSprites = new Sprite[4];

    [Header("Harvesting")]
    [Tooltip("The item needed to harvest this item")]
    public ItemApplication itemNeeded = ItemApplication.None;
}
