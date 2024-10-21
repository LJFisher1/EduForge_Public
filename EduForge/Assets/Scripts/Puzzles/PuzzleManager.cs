using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<MathPuzzle> mainPuzzles = new List<MathPuzzle>();  // List of all main puzzles
    public Door exitDoor;  // Reference to the exit door

    private int puzzlesCompleted = 0;  // Track the number of solved puzzles

    private void Start()
    {
        // Subscribe to the puzzle completion event for each main puzzle
        foreach (MathPuzzle puzzle in mainPuzzles)
        {
            if (puzzle != null)
            {
                puzzle.onPuzzleSolved.AddListener(OnPuzzleSolved);
            }
        }
    }

    // Called whenever a puzzle is solved
    private void OnPuzzleSolved(string puzzleID)
    {
        puzzlesCompleted++;
        Debug.Log($"Puzzle {puzzleID} solved! {puzzlesCompleted}/{mainPuzzles.Count} puzzles completed.");

        // Check if all puzzles are completed
        if (puzzlesCompleted >= mainPuzzles.Count)
        {
            UnlockExitDoor();
        }
    }

    // Unlocks the exit door
    private void UnlockExitDoor()
    {
        if (exitDoor != null)
        {
            exitDoor.UnlockDoor();
            Debug.Log("All puzzles solved! Exit door unlocked.");
        }
    }
}