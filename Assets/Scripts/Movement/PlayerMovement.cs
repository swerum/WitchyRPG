using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool canMove = true;
    [SerializeField] float playerSpeed = 0.01f;
    Rigidbody2D rb;
    private void Start() { rb = GetComponent<Rigidbody2D>(); }

    void FixedUpdate()
    {
        if (!canMove) return;
        //inputs can be changed under Edit --> Project Settings --> Input Manager
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal")*playerSpeed, Input.GetAxis("Vertical")*playerSpeed);
        rb.velocity = movement;
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
