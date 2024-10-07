using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompletionHandler : MonoBehaviour
{
    public MathPuzzle linkedPuzzle;  // Now references MathPuzzle (your abstract base class)
    public Door linkedDoor;          // The door this puzzle should unlock

    private void Start()
    {
        // Make sure the puzzle event listener is set up
        if (linkedPuzzle != null)
        {
            linkedPuzzle.onPuzzleSolved.AddListener(OnPuzzleSolved); // Subscribe to puzzle solved event
        }
    }

    // This method is triggered when the puzzle is solved
    private void OnPuzzleSolved(string puzzleID)
    {
        Debug.Log("Puzzle solved with ID: " + puzzleID);

        // If the door is linked, unlock it directly
        if (linkedDoor != null)
        {
            linkedDoor.UnlockDoor();
            Debug.Log("Door unlocked for puzzle ID: " + puzzleID);
        }
    }
}