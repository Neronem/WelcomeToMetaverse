using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 5f;
    private Rigidbody2D _rigidbody;
    private Vector2 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = movement.normalized * moveSpeed;
    }

    private void LateUpdate()
    {
        
    }
}
