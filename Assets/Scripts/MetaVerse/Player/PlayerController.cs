using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    
    public float moveSpeed = 5f;
    private Vector2 movement;
    
    public bool isStopped = false;
        
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        
        bool isMoving = movement != Vector2.zero;
        _animator.SetBool("IsMove", isMoving);
    }

    private void FixedUpdate()
    {
        if (isStopped) return;
        _rigidbody.velocity = movement.normalized * moveSpeed;
    }

    public void StopMovement()
    {
        isStopped = true;
        _rigidbody.velocity = Vector2.zero;
    }

    public void StartMovement()
    {
        isStopped = false;
    }
}
