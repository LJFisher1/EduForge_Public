using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<MathPuzzle> mainPuzzles = new List<MathPuzzle>();  // List of all main puzzles
    [SerializeField] public Door exitDoor;  // Reference to the exit door

    private int puzzlesCompleted = 0;  // Track the number of solved puzzles
    private HashSet<string> completedPuzzles = new HashSet<string>();  // Track completed puzzle IDs

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
        // Check if the puzzle has already been completed
        if (!completedPuzzles.Contains(puzzleID))
        {
            // Mark puzzle as completed
            completedPuzzles.Add(puzzleID);
            puzzlesCompleted++;
            Debug.Log($"Puzzle {puzzleID} solved! {puzzlesCompleted}/{mainPuzzles.Count} puzzles completed.");

            // Check if all puzzles are completed
            if (puzzlesCompleted >= mainPuzzles.Count)
            {
                UnlockExitDoor();
            }
        }
        else
        {
            Debug.Log($"Puzzle {puzzleID} has already been completed and will not be counted again.");
        }
    }

    // Unlocks the exit door
    private void UnlockExitDoor()
    {
        if (exitDoor != null)
        {
            Debug.Log("UnlockExitDoor: Exit door is assigned and will be unlocked.");
            exitDoor.UnlockDoor();
            Debug.Log("All puzzles solved! Exit door unlocked.");
        }
        else
        {
            Debug.LogError("UnlockExitDoor: Exit door is not assigned!");
        }
    }
}