using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public Transform cameraTarget;
    public Vector3 offset;

    private void Update()
    {
        transform.position = new Vector3(cameraTarget.position.x + offset.x, cameraTarget.position.y + offset.y, transform.position.z);
    }
}