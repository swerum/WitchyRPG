using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //SerializeField means that you can access this variable in the editor
    [SerializeField] Transform follow = null;
    [SerializeField] float spd = 0.1f;

    public enum MovementType { Following, WASD };
    MovementType movementType = MovementType.Following;
    public MovementType Movement { set { movementType = value; } }

    float z = -10;

    private void Start() {
        z = transform.position.z;
        transform.position = follow.transform.position;
    }

    void Update()
    {
        Vector2 pos = transform.position;
        float x;
        float y;
        if (movementType == MovementType.Following)
        {
            Vector2 dest = follow.transform.position;
            x = pos.x + Utility.GetXDirection(pos, dest, spd) * spd;
            y = pos.y + Utility.GetYDirection(pos, dest, spd) * spd;
        } else
        {
            x = pos.x + Input.GetAxis("Horizontal") * spd;
            y = pos.y + Input.GetAxis("Vertical") * spd;
        }
        transform.position = new Vector3(x, y, z);
    }
}
