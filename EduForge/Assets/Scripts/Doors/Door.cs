using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = true; // Determines if the door is locked
    public Vector3 openPositionOffset = new Vector3(0, 5, 0); // How far the door moves up/down
    public float moveSpeed = 2f; // Speed of the door's movement

    private bool isOpen = false; // Track if the door is open
    private bool isPlayerNear = false; // Track if the player is near the door
    private Vector3 closedPosition; // Store the initial position of the door
    private Vector3 openPosition; // Calculate the target open position

    private void Start()
    {
        // Store the closed position
        closedPosition = transform.position;

        // Calculate the open position by adding the offset
        openPosition = closedPosition + openPositionOffset;
    }

    private void Update()
    {
        // If the player is near, presses E, and the door is unlocked, toggle the door
        if (isPlayerNear && !isLocked && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }

        // Smoothly move the door to the target position (either open or closed)
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen; // Toggle the door state
    }

    public void UnlockDoor()
    {
        isLocked = false; // Unlock the door
        Debug.Log("The door is unlocked.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player near the door.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player left the door.");
        }
    }
}