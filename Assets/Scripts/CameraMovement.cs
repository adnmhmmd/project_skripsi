//	Created by: Sunny Valley Studio 
//	https://svstudio.itch.io

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera gameCamera;
    public float cameraMovementSpeed = 5;

    private void Start()
    {
        gameCamera = GetComponent<Camera>();
    }

    public void MoveCamera(Vector3 inputVector)
    {
        // Gerakkan kamera berdasarkan input yang diberikan
        gameCamera.transform.position += inputVector * Time.deltaTime * cameraMovementSpeed;
    }
}
