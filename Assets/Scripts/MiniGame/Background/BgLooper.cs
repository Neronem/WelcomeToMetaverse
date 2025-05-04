using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BgLooper : MonoBehaviour
{
    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    public int BgSize = 18;
    public int numBgCount = 5;
    public int numFloorCount = 5;
    
    private void Start()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;
        
        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Background"))
        {
            Vector3 pos = other.transform.position;
            
            pos.x = BgSize * numBgCount;
            other.transform.position = pos;
            numBgCount++;
        }

        if (other.CompareTag("Ground"))
        {
            Vector3 pos = other.transform.position;
            
            pos.x = BgSize * numFloorCount;
            other.transform.position = pos;
            numFloorCount++;
        }
        
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
        
    }
}
