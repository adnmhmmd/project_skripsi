using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action<Vector3Int> OnMouseClick, OnMouseHold;
    public Action OnMouseUp;

    [SerializeField]
    Camera mainCamera;
    public LayerMask groundMask;

    private Vector2 cameraMovementVector;
    private Vector2 lastMousePosition;

    public Vector2 CameraMovementVector
    {
        get { return cameraMovementVector; }
    }

    private Vector3Int? RaycastGround()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            return positionInt;
        }
        return null;
    }

    private void CheckClickHoldEvent()
    {
        if (Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            var position = RaycastGround();
            if (position != null)
                OnMouseHold?.Invoke(position.Value);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) // Jika mouse sedang di-drag
        {
            Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            cameraMovementVector = currentMousePosition - lastMousePosition; // Perbedaan posisi mouse
            lastMousePosition = currentMousePosition;
        }
        else
        {
            lastMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); // Simpan posisi awal saat mouse dilepas
        }
    }
}
