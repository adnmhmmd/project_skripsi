using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public InputManager inputManager;

    private void Start()
    {
        inputManager.OnMouseClick += HandelMouseClick;
    }

    private void HandelMouseClick(Vector3Int position)
    {
        Debug.Log(position);
    }

    private void Update()
    {
        // Kirimkan pergerakan kamera berdasarkan input dari InputManager
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
    }
}
