using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //SerializeField means that you can access this variable in the editor
    [SerializeField] Transform follow = null;
    [SerializeField] float spd = 0.1f;
    //[SerializeField] Bounds worldBounds = new Bounds();

    float z = -10;

    private void Start() {
        z = transform.position.z;
        transform.position = follow.transform.position;
    }

    void Update()
    {
        Vector2 pos = transform.position;
        Vector2 dest = follow.transform.position;
        float x = pos.x + Utility.GetXDirection(pos, dest, spd) * spd;
        float y = pos.y + Utility.GetYDirection(pos, dest, spd) * spd;
        transform.position = new Vector3(x, y, z);
    }
}
