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
        if (Input.GetMouseButtonDown(0)) Debug.Log(mousePos + " is snapped to " + Utility.SnapToGrid(mousePos, 0.08f));
        mousePos = Utility.SnapToGrid(mousePos, 0.08f);
        transform.position = mousePos + new Vector2(-0.04f, 0.04f);
    }
}
