using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // 쫓을 대상
    public Vector3 offset; // 카메라 위치
    public float smoothSpeed = 0.2f; //따라갈 속도
    
    public Vector2 minBounds, maxBounds; // 카메라 이동의 최소, 최대 좌표

    private void Start()
    { // 현재 타겟에서 카메라까지의 위치 차이 계산
        offset = new Vector3(0.45f, -0.4f, -10f);
    }

    private void LateUpdate()
    {
        if (target == null) return;
        
        // 위에서 구했던 위치차이를 끝까지 유지 (스토킹)
        Vector3 desiredPosition = target.position + offset;
        
        // 최대, 최소 범위 밖으로 못벗어나게하기
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        // Lerp 사용하여 목표지점까지 부드럽게 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
