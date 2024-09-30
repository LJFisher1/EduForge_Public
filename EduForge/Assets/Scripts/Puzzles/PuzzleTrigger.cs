using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public MathPuzzle puzzle; // Reference to the puzzle
    public PuzzleInputController puzzleInputController;
    private bool isPlayerInRange = false;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Set the current puzzle in PuzzleInputController
            if (puzzleInputController != null)
            {
                puzzleInputController.currentPuzzle = puzzle;
                Debug.Log("Current Puzzle has been updated to: " + puzzle.name);
            }

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