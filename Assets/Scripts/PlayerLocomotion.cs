using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerig;

    public float movementSpeed = 5.0f;
    public float rotationSpeed = 10.0f;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerig = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= movementSpeed;
        Vector3 movementVelocity = moveDirection;
        playerig.AddForce(movementVelocity,ForceMode.VelocityChange);
    }

    private void HandleRotation()
    {
        Vector3 targetDirction = Vector3.zero;

        targetDirction = cameraObject.forward * inputManager.verticalInput;
        targetDirction = targetDirction + cameraObject.right * inputManager.horizontalInput;
        targetDirction.Normalize();
        targetDirction.y = 0;

        if(targetDirction == Vector3.zero)
        {
            targetDirction = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirction);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
