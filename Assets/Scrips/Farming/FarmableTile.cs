using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmableTile : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite = null;

    //private variables
    bool            watered     = false;
    bool            harvestable = false;

    int             daysSincePlanting = 0;
    int             currentPlantSpriteIndex = -1;

    FarmAction      farmActionTaken  = FarmAction.Nothing;

    PlantInfo       currentPlant      = null;
    SpriteRenderer  spriteRenderer;


    private void Start() { spriteRenderer = GetComponent<SpriteRenderer>(); }

    public void PlowField(Sprite plowedTileSprite)
    {
        Debug.Log(name + " is being plowed");
        //if something is already there, destroy it
        currentPlant = null;
        harvestable = false;
        spriteRenderer.sprite = plowedTileSprite;
        farmActionTaken = FarmAction.Plow;
    }

    public void PlantSomething(PlantInfo plant)
    {
        if (farmActionTaken != FarmAction.Plow) return;
        spriteRenderer.sprite = plant.plantSprites[0];
        currentPlantSpriteIndex = 0;
        currentPlant = plant;
        daysSincePlanting = 0;
        harvestable = false;
        farmActionTaken = FarmAction.Plant;
    }

    [ContextMenu("Water Plant")]
    public void WaterPlant()
    {
        if (farmActionTaken != FarmAction.Plant) return;
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
            Debug.Log("Days: " + daysSincePlanting + " and new sprite index: " + newIndex);
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
        //put plant in inventory
        currentPlant = null;
        spriteRenderer.sprite = defaultSprite;
        farmActionTaken = FarmAction.Nothing;
        spriteRenderer.sprite = defaultSprite;
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
