using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidbody;

    public float startSpeed = 2f;
    public float moveSpeed = 0.2f;
    public float currentSpeed;

    public float jumpForce = 7f;
    private int jumpCount = 0;
    public int maxJumpCount = 2;
    
    private bool isGround = false;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        currentSpeed = startSpeed;
    }

    private void Update()
    {
        currentSpeed += Time.deltaTime * moveSpeed;
        rigidbody.velocity = new Vector2(currentSpeed, rigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            // velocity y값초기화 일부러 안하기 (더블점프할때는 그전에 내려오던 값 + 점프력으로 조작감 up)
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //forceMode2D. Impulse는 즉시 한번 세게 가한다는뜻 (점프에 좋다고 함)
            if (jumpCount == 1)
            {
                animator.SetBool("IsDoubleJump", true);
            }
            jumpCount++;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsDoubleJump", false);
            isGround = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}

