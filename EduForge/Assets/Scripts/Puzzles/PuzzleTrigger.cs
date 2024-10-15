using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public MathPuzzle puzzle; // Reference to the puzzle
    public PuzzleInputController puzzleInputController;
    private bool isPlayerInRange = false;
    public TextbookSystem textbookSystem;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Ensure puzzle is not null
            if (puzzle == null)
            {
                Debug.LogError("Puzzle reference is not assigned.");
                return;
            }

            // Ensure PuzzleInputController is assigned and not null
            if (puzzleInputController != null)
            {
                puzzleInputController.currentPuzzle = puzzle;
                Debug.Log("Current Puzzle has been updated to: " + puzzle.name);
            }
            else
            {
                Debug.LogError("PuzzleInputController reference is not assigned.");
                return;
            }

            // Start the puzzle
            puzzle.StartPuzzle();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}