using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputmanager;

    public Transform targetTrans; // The object the camera will follow
    public Transform cameraPivot; // The object the camera uses to pivot
    public Transform cameraTransform; // The transform of the actual camera object in the scene 
    public LayerMask collisionLayers; // The layer we want our camera to collide with
    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPos;

    public float cameraCollisionOffset = 0.2f; // How much the camera will jump off of objects its colliding with
    public float minimumCollisionOffset = 0.2f;
    public float cameraCollisionRadius = 2.0f;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2.0f;
    public float cameraPivotSpeed = 2.0f;

    public float lookAngle; //camera look up & down
    public float pivotAngle; //camera look left & right
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;

    private void Awake()
    {
        targetTrans = FindObjectOfType<PlayerManager>().transform;
        inputmanager = FindObjectOfType<InputManager>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    private void FollowTarget()
    {
        Vector3 targetPos = Vector3.SmoothDamp
            (
            transform.position, 
            targetTrans.position, 
            ref cameraFollowVelocity,
            cameraFollowSpeed
            );
        transform.position = targetPos;
    }
    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;
        lookAngle += (inputmanager.cameraInputX * cameraLookSpeed);
        pivotAngle -= (inputmanager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle,minimumPivotAngle, maximumPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 dirction = cameraTransform.position - cameraPivot.position;
        dirction.Normalize();

        if (Physics.SphereCast
            (
            cameraPivot.transform.position, 
            cameraCollisionRadius, 
            dirction,
            out hit,
            Mathf.Abs(targetPosition),
            collisionLayers
            )
            )
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = -(distance - cameraCollisionOffset);
        }
        if(Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition -= minimumCollisionOffset;
        }
        cameraVectorPos.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPos;
    }
}
