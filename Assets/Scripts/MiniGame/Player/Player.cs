using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidbody;

    public float startX;
    public float playerWalked = 0f;
    public float bestPlayerWalked = 0f;
    
    public float startSpeed = 2f;
    public float moveSpeed = 0.2f;
    public float currentSpeed;
    
    public float jumpForce = 7f;
    private int jumpCount = 0;
    public int maxJumpCount = 2;
    
    private bool isGround = false;
    private bool isDead = false;
    
    public bool IMTHEGOD = false;
    
    private const string BestScoreKey =  "BestScore";
    
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip deathSound;
    
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        currentSpeed = startSpeed;
        
        startX = transform.position.x;
        
        bestPlayerWalked = PlayerPrefs.GetFloat(BestScoreKey, 0);
        
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isDead)
        {
            // 게임 오버
            UpdateScore();
            UIManager.instance.GameOverUIAppear();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UIManager.instance.GameOverUIDisappear();
                GameManager.instance.ReStartGame();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                ChangeScene.instance.FadeToScene("Metaverse");
            }
        }
        else
        {
            currentSpeed += Time.deltaTime * moveSpeed;
            rigidbody.velocity = new Vector2(currentSpeed, rigidbody.velocity.y);
            playerWalked = transform.position.x - startX;
            
            if (!IMTHEGOD)
            {
                if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
                {
                    // velocity y값초기화 일부러 안하기 (더블점프할때는 그전에 내려오던 값 + 점프력으로 조작감 up)
                    rigidbody.AddForce(Vector2.up * jumpForce,
                        ForceMode2D.Impulse); //forceMode2D. Impulse는 즉시 한번 세게 가한다는뜻 (점프에 좋다고 함)
                    if (jumpCount == 1)
                    {
                        animator.SetBool("IsDoubleJump", true);
                    }
                    audioSource.PlayOneShot(jumpSound);
                    jumpCount++;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rigidbody.AddForce(Vector2.up * jumpForce,
                        ForceMode2D.Impulse);
                    if (jumpCount == 1)
                    {
                        animator.SetBool("IsDoubleJump", true);
                    }
                    
                    audioSource.PlayOneShot(jumpSound);
                    jumpCount++;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if (IMTHEGOD) return;
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsDoubleJump", false);
            isGround = true;
            jumpCount = 0;
            
            audioSource.PlayOneShot(landSound);
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
            audioSource.PlayOneShot(deathSound);
            animator.SetTrigger("IsDie");
            GameManager.instance.GameOver();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    void UpdateScore()
    {
        if (bestPlayerWalked < playerWalked)
        {
            bestPlayerWalked = playerWalked;
            
            PlayerPrefs.SetFloat(BestScoreKey, bestPlayerWalked);
        }
    }
}

