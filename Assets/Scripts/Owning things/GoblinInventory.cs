using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinInventory : Inventory
{
    [Tooltip("Where in relation to the goblin its inventory is.")]
    [SerializeField] Vector2 offset = Vector2.zero;

    Transform myGoblin;
    public Transform Goblin { set { myGoblin = value; } }

    Camera main;

    private void Start()
    {
        main = Camera.main;
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        transform.SetParent(canvas.transform);
    }

    void Update()
    {
        Vector3 uiPos = main.WorldToScreenPoint(myGoblin.position);
        Vector2 newPosition = new Vector2(uiPos.x + offset.x, uiPos.y + offset.y);
        transform.position = newPosition;
    }
}
