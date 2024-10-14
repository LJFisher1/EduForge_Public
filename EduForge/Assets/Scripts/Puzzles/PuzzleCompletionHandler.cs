using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompletionHandler : MonoBehaviour
{
    public MathPuzzle linkedPuzzle;  // Reference to the puzzle
    public List<Door> linkedDoors = new List<Door>();  // List of doors this puzzle unlocks

    private void Start()
    {
        if (linkedPuzzle != null)
        {
            linkedPuzzle.onPuzzleSolved.AddListener(OnPuzzleSolved);  // Subscribe to puzzle solved event
        }
    }

    // Triggered when the puzzle is solved
    private void OnPuzzleSolved(string puzzleID)
    {
        Debug.Log("Puzzle solved! Unlocking doors...");

        // Unlock all doors linked to this puzzle
        foreach (Door door in linkedDoors)
        {
            if (door != null)
            {
                door.UnlockDoor();
            }
        }
    }
}