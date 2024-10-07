using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = true; // Start with the door locked
    public float openRotationAngle = 90f; // How much the door will rotate to "open"
    private bool isOpen = false; // Track if the door is open
    private bool isPlayerNear = false; // Track if the player is near the door
    private Quaternion closedRotation; // Save the closed position for later
    private Quaternion openRotation; // Save the target open position

    private void Start()
    {
        // Store the original rotation of the door as the "closed" state
        closedRotation = transform.rotation;

        // Calculate the target open rotation
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openRotationAngle, 0));
    }

    private void Update()
    {
        // Check if the player is near and presses the E key, and the door is unlocked
        if (isPlayerNear && !isLocked && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }
    }

    private void ToggleDoor()
    {
        if (isOpen)
        {
            // Close the door (set rotation back to the closed state)
            transform.rotation = closedRotation;
        }
        else
        {
            // Open the door (rotate by the openRotationAngle)
            transform.rotation = openRotation;
        }
        isOpen = !isOpen; // Toggle the state
    }

    public void UnlockDoor()
    {
        isLocked = false; // Unlock the door, allows player to open it
        Debug.Log("The door is unlocked.");
    }

    // This detects when the player is near the door
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player has the tag "Player"
        {
            isPlayerNear = true;
            Debug.Log("Player near the door.");
        }
    }

    // This detects when the player moves away from the door
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player left the door.");
        }
    }
}