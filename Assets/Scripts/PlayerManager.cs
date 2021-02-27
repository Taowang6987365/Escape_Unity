using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerLocomotion playerlocomotion;
    CameraManager cameramanager;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerlocomotion = GetComponent<PlayerLocomotion>();
        cameramanager = FindObjectOfType<CameraManager>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerlocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameramanager.HandleAllCameraMovement();
    }
}
