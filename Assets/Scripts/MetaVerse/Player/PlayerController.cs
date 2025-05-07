using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    public GameObject vehicle;
    public SpriteRenderer vehicleSprite;
    public VehicleController vehicleController;
    public SpriteRenderer[] characterSprites;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    
    public float moveSpeed = 5f;
    private Vector2 movement;
    
    private bool isStopped = false;
    private bool isRiding = false;
    private bool isLeaderBoardShow = false;
    
    public LeaderBoard leaderBoard;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        vehicleSprite = vehicle.GetComponent<SpriteRenderer>();
        vehicleController = vehicle.GetComponent<VehicleController>();
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isLeaderBoardShow)
            {
                leaderBoard.HideLeaderBoard();
                isLeaderBoardShow = false;
            }
            else
            {
                leaderBoard.ShowLeaderBoard();
                isLeaderBoardShow = true;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isRiding)
            {
                vehicleController.FinishRide();
                foreach (SpriteRenderer sprite in characterSprites)
                {
                    sprite.transform.position += new Vector3(0, -1.5f, 0);
                }
                isRiding = false;
                moveSpeed -= 10f;
            }
            else
            {
                vehicleController.LetsRide();
                foreach (SpriteRenderer sprite in characterSprites)
                {
                    sprite.transform.position += new Vector3(0, 1.5f, 0);
                }
                isRiding = true;
                moveSpeed += 10f;
            }
        }
        
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
            vehicleSprite.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
            vehicleSprite.flipX = true;
        }
        
        bool isMoving = movement != Vector2.zero;
        if (!isRiding)
        {
            _animator.SetBool("IsMove", isMoving); 
        }
        else
        {
            _animator.SetBool("IsMove", false);
        }
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

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
