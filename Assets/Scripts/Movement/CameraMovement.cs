using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    //SerializeField means that you can access this variable in the editor
    [SerializeField] Transform follow = null;
    public Transform Follow { set { follow = value; } }
    [Tooltip("How fast can the camera move.")]
    [SerializeField] float spd = 0.1f;

    public enum MovementType { Following, WASD };
    MovementType movementType = MovementType.Following;
    public MovementType Movement { set { movementType = value; } }

    float z = -10;
    Vector2 max;
    Vector2 min;

    private void Start() {
        transform.position = follow.transform.position;
    }

    void Update()
    {
        Vector2 pos = transform.position;
        float deltaX;
        float deltaY;
        //check how camera should move based on movement type
        if (movementType == MovementType.Following)
        {
            Vector2 dest = follow.transform.position;
            deltaX = Utility.GetXDirection(pos, dest, spd) * spd;
            deltaY = Utility.GetYDirection(pos, dest, spd) * spd;
        } else
        {
            deltaX = Input.GetAxis("Horizontal") * spd;
            deltaY = Input.GetAxis("Vertical") * spd;
        }
        //check if camera out of bounds
        if (pos.x + deltaX > max.x || pos.x + deltaX < min.x) deltaX = 0;
        if (pos.y + deltaY > max.y || pos.y + deltaY < min.y) deltaY = 0;
        transform.position = new Vector3(pos.x + deltaX, pos.y + deltaY, z);
    }

    /// <summary>
    /// Set the Bounds for the camera
    /// </summary>
    /// <param name="bounds"></param>
    public void SetBounds(Bounds bounds)
    {
        Camera cam = GetComponent<Camera>();
        float height = cam.orthographicSize;
        float width = height * cam.aspect;
        Vector3 viewportSize = new Vector3(width, height, 0);
        max = bounds.max - viewportSize;
        min = bounds.min + viewportSize;
        transform.position = bounds.center;
    }
}
