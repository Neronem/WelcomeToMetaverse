using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    
    public float moveSpeed = 5f;
    private Vector2 movement;
    
    public bool isStopped = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (PlayerSkinSelect.instance.isPlayerSelected)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _animator = GetComponentInChildren<Animator>();
            
            transform.position = new Vector3(0, 0.4f, 0);
        }
    }

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
    
        if (currentScene == "MiniGame")
        {
            gameObject.SetActive(false);
            return;
        }
        if (!PlayerSkinSelect.instance.isPlayerSelected) return;
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
        if (!PlayerSkinSelect.instance.isPlayerSelected) return;
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MiniGame")
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
       
            CameraController cameraController = FindObjectOfType<CameraController>();
            if (cameraController != null)
            {
                cameraController.SetTarget(transform);
            }
        }
    }

}
