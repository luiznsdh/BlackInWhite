using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    [SerializeField]
    private float smoothTime = 0.3f;

    public Vector3 cameraOffset = new Vector3(0, 3, -10);

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        if (player)
        {
            Vector3 targetPosition = player.TransformPoint(cameraOffset);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            if (this.transform.position != Vector3.zero)
                this.transform.position = Vector3.zero;
        }      
    }
}
