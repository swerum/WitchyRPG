using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    [SerializeField] Bounds bounds = new Bounds();

    private void Start()
    {
        transform.position = bounds.center;
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        if (colliders.Length != 4) Debug.LogError("You need exactly four colliders.");
        //left 
        colliders[0].size   = new Vector2(0.1f, bounds.size.y);
        colliders[0].offset = new Vector2(-bounds.size.x / 2, 0);
        //right
        colliders[1].size   = new Vector2(0.1f, bounds.size.y);
        colliders[1].offset = new Vector2(bounds.size.x / 2, 0);
        // up
        colliders[2].size = new Vector2(bounds.size.x, 0.1f);
        colliders[2].offset = new Vector2(0, bounds.size.y / 2);
        //down
        colliders[3].size = new Vector2(bounds.size.x, 0.1f);
        colliders[3].offset = new Vector2(0, -bounds.size.y / 2);
        //
        foreach(Collider2D c in colliders) { c.isTrigger = false; }
        //give camera the bounds
        Camera.main.GetComponent<CameraMovement>().SetBounds(bounds);
    }

}
