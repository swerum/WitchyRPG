using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //SerializeField means that you can access this variable in the editor
    [SerializeField] Transform follow = null;
    [SerializeField] float spd = 0.1f;

    float z = -10;

    private void Start() { z = transform.position.z; }

    void Update()
    {
        Vector2 pos = transform.position;
        Vector2 dest = follow.transform.position;
        float xDirection = Utility.GetXDirection(pos, dest, spd);
        float yDirection = Utility.GetYDirection(pos, dest, spd);
        transform.position = new Vector3(pos.x + xDirection * spd, pos.y + yDirection * spd, z);
    }
}
