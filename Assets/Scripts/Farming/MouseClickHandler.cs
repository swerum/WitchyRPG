using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemAction { Nothing, Plow, Plant, Harvest, Water, DeafenSound};

public class MouseClickHandler : MonoBehaviour
{
    #region variables and initializing

    ItemAction clickAction = ItemAction.Nothing;
    public ItemAction ClickAction { set { clickAction = value; } }
    Camera main;
    FarmableTile currentFarmableTile = null;
    Goblin currentGoblin;

    PlantInfo plant = null;
    public PlantInfo Plant { set { plant = value; } }

    private void Start() { main = Camera.main; }
    #endregion

    #region singleton
    private static MouseClickHandler _instance;

    public static MouseClickHandler Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    void Update()
    {
        //move mouse tile
        Vector2 mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = Utility.SnapToGrid(mousePos);
        transform.position = mousePos;
        //click tile and do action
        ClickOnFarmableTile(currentFarmableTile);
        ClickOnGoblin(currentGoblin);

    }

    /// <summary>
    /// if you clicked on a farmable tile, do the current ItemAction on it
    /// </summary>
    private void ClickOnFarmableTile(FarmableTile farmableTile)
    {
        if (Input.GetMouseButtonDown(0) && farmableTile != null)
        {
            //do farm action with non-null farmableTile
            switch (clickAction)
            {
                case ItemAction.Nothing: break;
                case ItemAction.Plow: farmableTile.PlowField(); break;
                case ItemAction.Plant: farmableTile.PlantSomething(plant); PlayerInventory.Instance.ReduceCurrentItem(1); break;
                case ItemAction.Harvest: farmableTile.Harvest(); break;
                case ItemAction.Water: farmableTile.WaterPlant(); break;
            }
        }
    }

    /// <summary>
    /// This function gives goblins commands or items out of the players inventory if clicked on.
    /// </summary>
    /// <param name="goblin">The Goblin in question</param>
    private void ClickOnGoblin(Goblin goblin)
    {
        if (goblin == null) return;
        if (Input.GetMouseButtonDown(0))
        {
            Item item = PlayerInventory.Instance.GetCurrentItem();
            if (item == null) return;
            if (goblin.Inventory.AddToInventory(item))
                PlayerInventory.Instance.ReduceCurrentItem(1);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            goblin.GetComponent<DirectedMovement>().StartDoingChores();
        }
        else if (Input.GetMouseButtonDown(2))
        {
            goblin.Inventory.GiveLastItemBack();
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        FarmableTile farmableTile = collision.gameObject.GetComponent<FarmableTile>();
        if (farmableTile != null) { currentFarmableTile = farmableTile; }
        Goblin g = collision.gameObject.GetComponent<Goblin>();
        if (g != null) { currentGoblin = g; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FarmableTile farmableTile = collision.gameObject.GetComponent<FarmableTile>();
        if (farmableTile == currentFarmableTile) { currentFarmableTile = null; }
        Goblin g = collision.gameObject.GetComponent<Goblin>();
        if (g == currentGoblin) { currentGoblin = null; }
    }
}
