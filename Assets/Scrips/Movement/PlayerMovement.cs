using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 0.01f;

    void Update()
    {
        //inputs can be changed under Edit --> Project Settings --> Input Manager
        float xDirection = Input.GetAxis("PlayerRight");
        float yDirection = Input.GetAxis("PlayerUp");
        Vector2 pos = transform.position;
        transform.position = new Vector2(pos.x + xDirection * playerSpeed, pos.y + yDirection * playerSpeed);
    }
}
