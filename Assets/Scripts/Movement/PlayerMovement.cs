using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 0.01f;
    Rigidbody2D rb;
    private void Start() { rb = GetComponent<Rigidbody2D>(); }

    void FixedUpdate()
    {
        //inputs can be changed under Edit --> Project Settings --> Input Manager
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal")*playerSpeed, Input.GetAxis("Vertical")*playerSpeed);
        rb.velocity = movement;
    }
}
