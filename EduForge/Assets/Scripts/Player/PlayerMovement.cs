using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Movement speed of player
    public float sprintModifier = 2.0f; // Sprint modifier to increase speed
    public float lookSpeed = 2.0f; // Sensitivity of mouse look
    public Camera playerCamera;

    private CharacterController characterController;
    private float verticalRotation = 0.0f; // To track vertical camera rotation
    private bool isPuzzleMode = false; // Flag to check if puzzle mode is active

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is not assigned.");
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPuzzleMode)
        {
            MovePlayer();
            LookAround();
        }
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Debug logs for sprint status and speed
       // Debug.Log($"Is Sprinting: {isSprinting}");
       // Debug.Log($"Base Move Speed: {moveSpeed}");

        // Calculate direction based on input
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveDirection = moveDirection.normalized * moveSpeed;

        float currentSpeed = isSprinting ? moveSpeed * sprintModifier : moveSpeed;

        // Debug.Log($"Current Speed: {currentSpeed}");

        // Apply movement
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    void LookAround()
    {
        if (playerCamera == null)
            return;

        // Get mouse input for looking around
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Rotate the player horizontally with the mouse's X movement
        transform.Rotate(Vector3.up * mouseX);

        // Adjust vertical camera rotation
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        // Apply the new vertical rotation to the camera
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    public void TogglePuzzleMode(bool isPuzzleActive)
    {
        isPuzzleMode = isPuzzleActive;

        if (isPuzzleMode)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
