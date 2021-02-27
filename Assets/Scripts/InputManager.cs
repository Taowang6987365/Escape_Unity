using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerController playerController;
    AnimatorManager animatormanager;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;
    public bool open_Door;

    private float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    private GameManager gm;

    private void Awake()
    {
        animatormanager = GetComponent<AnimatorManager>();
        gm = GameManager.Instance;
    }

    private void OnEnable()
    {
        if(playerController == null)
        {
            playerController = new PlayerController();

            playerController.PlayerAction.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerController.PlayerAction.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }
        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        //HandleActionInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatormanager.UpdateAnimatorValues(0, moveAmount);
    }

    public void OpenDoor(InputAction.CallbackContext context)
    {
        open_Door = context.performed;
        if (open_Door && GameManager.canOpen)
        {
            gm.Load_Scene();
        }

    }
}
