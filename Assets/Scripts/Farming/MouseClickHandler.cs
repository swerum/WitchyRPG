using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemAction { Nothing, Plow, Plant, Harvest, Water, DeafenSound};

public class MouseClickHandler : MonoBehaviour
{
    #region variables and initializing
    [SerializeField] Sprite plowedTileSprite = null;

    ItemAction clickAction = ItemAction.Nothing;
    public ItemAction ClickAction { set { clickAction = value; } }
    Camera main;
    FarmableTile currentFarmableTile = null;
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
        if (Input.GetMouseButtonDown(0))
        {
            ClickOnFarmableTile(currentFarmableTile);
        }
    }

    /// <summary>
    /// if you clicked on a farmable tile, do the current ItemAction on it
    /// </summary>
    private void ClickOnFarmableTile(FarmableTile farmableTile)
    {
        if (farmableTile == null) return;
        //do farm action with non-null farmableTile
        switch (clickAction)
        {
            case ItemAction.Nothing: break;
            case ItemAction.Plow: farmableTile.PlowField(plowedTileSprite); break;
            case ItemAction.Plant: farmableTile.PlantSomething(plant); Inventory.Instance.ReduceCurrentItem(1); break;
            case ItemAction.Harvest: farmableTile.Harvest(); break;
            case ItemAction.Water: farmableTile.WaterPlant(); break;
        }
    }

    /// <summary>
    /// Get the GameObject that the mouse clicked on
    /// </summary>
    /// <returns>the GameObject that the mouse clicked on</returns>
    private GameObject GettMouseClickObject()
    {
        Vector3 mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider == null) return null;
        return hit.collider.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FarmableTile farmableTile = collision.gameObject.GetComponent<FarmableTile>();
        if (farmableTile != null) { currentFarmableTile = farmableTile; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FarmableTile farmableTile = collision.gameObject.GetComponent<FarmableTile>();
        if (farmableTile == currentFarmableTile) { currentFarmableTile = null; }
    }

}
