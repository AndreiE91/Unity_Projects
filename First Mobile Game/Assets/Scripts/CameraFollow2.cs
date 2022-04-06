using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPositionFunc;
    public Transform playerTransform;

    private void Start()
    {
        Setup(() => playerTransform.position);
    }

    void Update()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;
        transform.position = cameraFollowPosition;
    }
    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }
}
