using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FarmAction { Nothing, Plow, Plant, Harvest};

public class MouseClickHandler : MonoBehaviour
{
    [SerializeField] Sprite plowedTileSprite = null;
    [SerializeField] FarmAction farmAction = FarmAction.Nothing;
    [SerializeField] PlantInfo plant = null;
    Camera main;

    private void Start() { main = Camera.main; }

    void Update()
    {
        //move mouse tile
        Vector2 mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = Utility.SnapToGrid(mousePos);
        transform.position = mousePos;
        //click tile and do action
        if (Input.GetMouseButtonDown(0))
        {
            //get clicked on object
            GameObject obj = GettMouseClickObject();
            if (obj == null) return;
            //click on the object
            ClickOnFarmableTile(obj);
        }
    }

    /// <summary>
    /// if you clicked on a farmable tile, do the current farmaction on it
    /// </summary>
    private void ClickOnFarmableTile(GameObject obj)
    {
        //get FarmableTile
        FarmableTile farmableTile = obj.GetComponent<FarmableTile>();
        if (farmableTile == null) return;
        //do farm action with non-null farmableTile
        switch (farmAction)
        {
            case FarmAction.Nothing: break;
            case FarmAction.Plow: farmableTile.PlowField(plowedTileSprite); break;
            case FarmAction.Plant: farmableTile.PlantSomething(plant); break;
            case FarmAction.Harvest: farmableTile.Harvest(); break;
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

}
