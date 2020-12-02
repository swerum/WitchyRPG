using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseClickHandler : MonoBehaviour
{
    Camera main;
    private void Start() { main = Camera.main; }

    void Update()
    {
        Vector2 mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        //if (Input.GetMouseButtonDown(0)) Debug.Log(mousePos + " is snapped to " + Utility.SnapToGrid(mousePos));
        mousePos = Utility.SnapToGrid(mousePos);
        transform.position = mousePos;
    }
}
