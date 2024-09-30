using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Variables to track the locked state and puzzle ID
    public bool isLocked = true;
    public string puzzleID; // This ID corresponds to a specific puzzle

    // Method to unlock the door
    public void UnlockDoor()
    {
        isLocked = false;
        OpenDoor();
    }

    // Method to open the door
    private void OpenDoor()
    {
        if (!isLocked)
        {
            // Play door opening animation or trigger logic to make the door move
            Debug.Log("The door is unlocked and opened!");
            // animate the door to rotate or move
            // transform.Rotate(0f, 90f, 0f); // Basic example for a hinge opening
        }
    }
}