using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementFollow : MonoBehaviour
{
    [Tooltip("The regular game element this UI object is following")]
    [SerializeField] Transform follow = null;
    public Transform Follow { set { follow = value; } }

    [Tooltip("How much should this object be offset from the object it is following.")]
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
