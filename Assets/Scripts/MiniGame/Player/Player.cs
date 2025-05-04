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
    }
}
