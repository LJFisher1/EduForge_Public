using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public bool isLocked = true;
    public bool showOrderNumber = false;  // Toggle for showing door order

    private TextMeshProUGUI doorLockText;
    private TextMeshProUGUI doorOrderText;

    private bool isOpen = false;  // Track if the door is open
    private Vector3 closedPosition;
    private Vector3 openPosition;
    public Vector3 openPositionOffset = new Vector3(0, 5, 0);  // Offset for opening door
    public float moveSpeed = 2f;

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
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
        UpdateLockStatus();  // Update lock status display when unlocked
        Debug.Log($"{gameObject.name} is unlocked.");
    }

    private void UpdateLockStatus()
    {
        if (doorLockText != null)
        {
            doorLockText.text = isLocked ? "Locked" : "Unlocked";
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
        if (other.CompareTag("Player") && !isLocked)
        {
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
        }
    }
}