﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAssigner : MonoBehaviour
{
    [SerializeField] GameObject markPrefab = null;
    [SerializeField] GameObject returnButton = null;

    Goblin goblin;
    public Goblin Goblin { set { goblin = value; } }
    Camera main;
    FarmableTile currentFarmableTile;
    GameObject button;

    List<FarmableTile>  tiles = new List<FarmableTile>();
    List<GameObject>    marks = new List<GameObject>();

    void Start()
    {
        main = Camera.main;
        PlayerMovement.Instance.Freeze();
        Camera.main.GetComponent<CameraMovement>().Movement = CameraMovement.MovementType.WASD;
        button = Instantiate(returnButton);
        button.GetComponent<ReturnFromTileAssigner>().TileAssigner = this;
        MouseClickHandler.Instance.gameObject.SetActive(false);
    }

    //check for mouse input
    void Update()
    {
        //move mouse tile
        Vector2 mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = Utility.SnapToGrid(mousePos);
        transform.position = mousePos;
        //click on farmable tile
        if (Input.GetMouseButtonDown(0))
        {
            if (currentFarmableTile == null) return;
            else if (tiles.Contains(currentFarmableTile)) RemoveFarmableTile(currentFarmableTile);
            else AddFarmableTile(currentFarmableTile);
        }
    }

    /// <summary>
    /// Destroys itself and the objects it has created for visualization
    /// </summary>
    public void DestroySelf()
    {
        Destroy(button);
        goblin.FarmableTiles = tiles;
        foreach (GameObject mark in marks) { Destroy(mark); }
        Destroy(gameObject);
    }

    //adds a tile to its list and marks is with a mark sprite
    private void AddFarmableTile(FarmableTile tile)
    {
        tiles.Add(tile);
        GameObject mark = Instantiate(markPrefab);
        mark.transform.position = tile.transform.position;
        marks.Add(mark);
    }

    //removes tile from its list and destroys the mark that was on it.
    private void RemoveFarmableTile(FarmableTile tile)
    {
        int index = tiles.FindIndex(x => x.Equals(tile));
        tiles.RemoveAt(index);
        GameObject mark = marks[index];
        marks.RemoveAt(index);
        Destroy(mark);
    }

    #region Keeps track of the farmable tile we're on
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
    #endregion
}
