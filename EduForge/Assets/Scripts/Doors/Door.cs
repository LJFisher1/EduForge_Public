using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public bool isLocked = true;
    public bool showOrderNumber = false;  // Toggle for showing door order
    public bool isFinalDoor = false; // Checks if the door is the final door

    private TextMeshProUGUI doorLockText;  // Updated to doorLockText
    private TextMeshProUGUI doorOrderText;

    private bool isOpen = false;  // Track if the door is open
    private bool isMoving = false;  // Track if the door is currently moving
    private Vector3 closedPosition;
    private Vector3 openPosition;
    public Vector3 openPositionOffset = new Vector3(0, 5, 0);  // Offset for opening door
    public float moveSpeed = 2f;

    public MainMenuController mainMenuController; // Reference to MainMenuController

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + openPositionOffset;

        // Find TextMeshPro components in the child Canvas
        doorLockText = transform.Find("DoorUI/DoorLockText").GetComponent<TextMeshProUGUI>();
        doorOrderText = transform.Find("DoorUI/DoorOrderText").GetComponent<TextMeshProUGUI>();

        UpdateLockStatus();
        ToggleDoorOrderVisibility();
    }

    private void Update()
    {
        if (isMoving)
        {
            // Move the door towards its target position (open or closed)
            Vector3 targetPosition = isOpen ? openPosition : closedPosition;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop moving if the door has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;  // Stop the door's movement
            }
        }
    }

    public void UnlockDoor()
    {
        if (!isLocked)
            return;  // Already unlocked, no further action needed

        isLocked = false;
        UpdateLockStatus();  // Update lock status display when unlocked
        Debug.Log($"{gameObject.name} is unlocked.");
    }

    private void UpdateLockStatus()
    {
        if (doorLockText != null)
        {
            doorLockText.text = isLocked ? "Locked" : "Unlocked";  // Update the lock status text
        }
    }

    private void ToggleDoorOrderVisibility()
    {
        if (doorOrderText != null)
        {
            doorOrderText.gameObject.SetActive(showOrderNumber);  // Toggle visibility only, keep the text constant
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLocked && !isMoving)
        {
            ToggleDoor();  // Toggle the door open or close

            if (isFinalDoor)
            {
                Debug.Log("Final door triggered.");
                if (mainMenuController != null)
                {
                    Debug.Log("MainMenuController found. Calling EndGame.");
                    mainMenuController.EndGame();
                }
                else
                {
                    Debug.LogError("MainMenuController is not assigned!");
                }
            }
        }
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;  // Toggle the open state
        isMoving = true;   // Begin moving the door
    }

}