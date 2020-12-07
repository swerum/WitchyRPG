using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementFollow : MonoBehaviour
{
    [SerializeField] Transform follow = null;
    public Transform Follow { set { follow = value; } }
    [SerializeField] Vector2 offset = Vector2.zero;
    Camera main;

    private void Start()
    {
        main = Camera.main;
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        transform.SetParent(canvas.transform);
    }

    void Update()
    {
        Vector3 uiPos = main.WorldToScreenPoint(follow.position);
        Vector2 newPosition = new Vector2(uiPos.x + offset.x, uiPos.y + offset.y);
        transform.position = newPosition;
    }
}
