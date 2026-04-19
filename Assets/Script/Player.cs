using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    SpriteRenderer sr;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");

        bool isMoving = moveInput != Vector2.zero;
        animator.SetBool("isMoving", isMoving);
        if (moveX > 0)
            sr.flipX = false; 
        else if (moveX < 0)
            sr.flipX = true; 
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
