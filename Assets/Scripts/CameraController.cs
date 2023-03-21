using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;
    public Transform farBackground, middleBackground, upperBackground;

    public float farCameraSpeed, middleCameraSpeed, upperCameraSpeed;
    public float yminHeight, ymaxHeight;
    public float xminHeight, xmaxHeight;

    public bool stopFollow;

    private Vector2 lastPos;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        
        if (!stopFollow)
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xminHeight, xmaxHeight), Mathf.Clamp(target.position.y, yminHeight, ymaxHeight), transform.position.z);

            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0) * farCameraSpeed;
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0) * middleCameraSpeed;

            farBackground.position += new Vector3(amountToMove.x, 0, 0) * farCameraSpeed;
            middleBackground.position += new Vector3(amountToMove.x, 0, 0) * middleCameraSpeed;
            upperBackground.position += new Vector3(amountToMove.x, 0, 0) * upperCameraSpeed;

            lastPos = transform.position;
        }
    }
}
