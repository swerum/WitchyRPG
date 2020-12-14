using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("How fast can the player walk. Careful: The player moves by adding force, so its speed will differ from the camera's speed for the same value.")]
    [SerializeField] float playerSpeed = 0.01f;

    bool            canMove         = true;
    Rigidbody2D     rb;
    Animator        animator;
    SpriteRenderer  spriteRenderer;

    private void Start()
    {
        rb              = GetComponent<Rigidbody2D>();
        animator        = GetComponent<Animator>();
        animator.speed  = 3;
        spriteRenderer  = GetComponent<SpriteRenderer>();
    }

    //add force to player, update animator
    void FixedUpdate()
    {
        if (!canMove) return;
        //inputs can be changed under Edit --> Project Settings --> Input Manager
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal")*playerSpeed, Input.GetAxis("Vertical")*playerSpeed);
        rb.velocity = movement;
        //set animations
        int yDirection = (int)(movement.y);
        animator.SetInteger("yDirection", yDirection);
        animator.SetInteger("xDirection", (int)movement.x);
        if (yDirection == 0 && movement.x < 0) spriteRenderer.flipX = false;
        else if (movement.x > 0) spriteRenderer.flipX = true;
    }

    public void Freeze()    { canMove = false; }
    public void UnFreeze()  { canMove = true; }

    #region singleton
    private static PlayerMovement _instance;

    public static PlayerMovement Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion
}
