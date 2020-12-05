using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmableTile : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite    = null;
    [SerializeField] Sprite plowedTileSprite = null;

    //private variables
    bool            watered                 = false;
    bool            harvestable             = false;

    int             daysSincePlanting       = 0;
    int             currentPlantSpriteIndex = -1;

    ItemAction      ItemActionTaken         = ItemAction.Nothing;

    PlantInfo       currentPlant            = null;
    SpriteRenderer  spriteRenderer;


    private void Start() { spriteRenderer = GetComponent<SpriteRenderer>(); }

    public void PlowField()
    {
        //if something is already there, destroy it
        currentPlant = null;
        harvestable = false;
        spriteRenderer.sprite = plowedTileSprite;
        ItemActionTaken = ItemAction.Plow;
    }

    public void PlantSomething(PlantInfo plant)
    {
        if (ItemActionTaken != ItemAction.Plow) return;
        spriteRenderer.sprite = plant.plantSprites[0];
        currentPlantSpriteIndex = 0;
        currentPlant = plant;
        daysSincePlanting = 0;
        harvestable = false;
        ItemActionTaken = ItemAction.Plant;
    }

    public void WaterPlant()
    {
        if (ItemActionTaken != ItemAction.Plant) return;
        watered = true;
        //add water overlay sprite?
    }

    [ContextMenu("Increment Days by One")]
    public void IncrementDay()
    {
        if (currentPlant != null && watered)
        {
            daysSincePlanting++;
            //check if the sprite index has changed
            int newIndex = GetPlantStage(daysSincePlanting);
            if (newIndex != currentPlantSpriteIndex)
            {
                currentPlantSpriteIndex = newIndex;
                spriteRenderer.sprite = currentPlant.plantSprites[newIndex];
                if (newIndex >= currentPlant.days.Length - 1) harvestable = true;
            }            
        }
        watered = false;
    }

    public void Harvest()
    {
        if (!harvestable) return;
        if (!PlayerInventory.Instance.ContainsItemType(currentPlant.itemNeeded))
        {
            //if we don't have the item needed
            Debug.Log("DEATH");
            return;
        }
        //put plant in Inventory.Instance
        spriteRenderer.sprite = defaultSprite;
        ItemActionTaken = ItemAction.Nothing;
        spriteRenderer.sprite = defaultSprite;
        PlayerInventory.Instance.AddToInventory(currentPlant.harvest);
        currentPlant = null;
        harvestable = false;
    }

    private int GetPlantStage(int numDays)
    {
        int[] days = currentPlant.days;
        for (int i = days.Length-1; i>= 0; i--)
        {
            if (numDays >= days[i]) return i+1;
        }
        return 0;
    }
}
