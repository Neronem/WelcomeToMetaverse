using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = 0.1f;

    public Transform ObstacleObject;

    public float highWidthPadding = 6f;
    public float lowWidthPadding = 4f;
    
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float height = Random.Range(lowPosY, highPosY);
        ObstacleObject.localPosition = new Vector3(0, height);
        
        float padding = Random.Range(lowWidthPadding, highWidthPadding);
        Vector3 newPosition = lastPosition + new Vector3(padding, 0);
        transform.position = newPosition;
        return newPosition;
    }
}
