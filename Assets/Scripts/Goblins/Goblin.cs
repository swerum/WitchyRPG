using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [Tooltip("What this goblin can do.")]
    [SerializeField] ItemAction goblinAction = ItemAction.Harvest;
    [Tooltip("the List of Farmable Tiles the goblins are supposed to visit.")]
    [SerializeField] List<FarmableTile> farmableTiles = new List<FarmableTile>();
    [Tooltip("The Prefab for the goblin Inventory")]
    [SerializeField] GameObject goblinInventoryPrefab = null;
    [Tooltip("The prefab for the goblin command menu.")]
    [SerializeField] GameObject goblinCommandMenuPrefab = null;


    PlantInfo plantInfo;
    GoblinInventory goblinInventory;
    public GoblinInventory Inventory { get { return goblinInventory; } }
    GoblinCommandMenu commandMenu;
    public GoblinCommandMenu Menu { get { return commandMenu; } }

    int indexOfPlant = -1;

    private void Start()
    {
        goblinInventory = Instantiate(goblinInventoryPrefab).GetComponent<GoblinInventory>();
        goblinInventory.GetComponent<UIElementFollow>().Follow = transform;
        commandMenu = Instantiate(goblinCommandMenuPrefab).GetComponent<GoblinCommandMenu>();
        commandMenu.Goblin = this;
        commandMenu.GetComponent<UIElementFollow>().Follow = transform;
        GetPlantSeed();
    }

    public Queue<FarmableTile> GetFarmableTileQueue()
    {
        Queue<FarmableTile> tileQueue = new Queue<FarmableTile>();
        foreach (FarmableTile farmableTile in farmableTiles) tileQueue.Enqueue(farmableTile);
        return tileQueue;
    }


    public void farmTile(FarmableTile farmableTile)
    {
        if (!goblinInventory.ContainsItemType(goblinAction)) return;
        switch(goblinAction)
        {
            case ItemAction.Harvest: farmableTile.Harvest(); break;
            case ItemAction.Plant:
                {
                    GetPlantSeed();
                    if (indexOfPlant == -1) return;
                    farmableTile.PlantSomething(plantInfo);
                    goblinInventory.ReduceItem(indexOfPlant, 1);
                    break;
                }
            case ItemAction.Plow: farmableTile.PlowField(); break;
            case ItemAction.Water: farmableTile.WaterPlant(); break;
        }
    }

    /// <summary>
    /// Set the new plant info
    /// </summary>
    /// <returns></returns>
    [ContextMenu("Get next plant seed.")]
    private void GetPlantSeed()
    {
        indexOfPlant = goblinInventory.GetFirstIndexOfElementOfType(goblinAction);
        if (indexOfPlant != -1) { plantInfo = goblinInventory.GetItemAtIndex(indexOfPlant).plant; }
        goblinInventory.UpdateAllSprites();
    }
}
