using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextbookTester : MonoBehaviour
{
    public TextMeshProUGUI textbookText; // Reference to the UI TextMeshProUGUI to display textbook content

    // Instance of the TextbookSystem class
    public TextbookSystem textbookSystem;

    private void Start()
    {
        // Find the TextbookSystem in the scene
        textbookSystem = FindObjectOfType<TextbookSystem>();
    }

    public void ShowEquationContent()
    {
        // Get content for the Equation puzzle type and display it
        textbookSystem.DisplayPuzzleInfo("Equations", textbookText);
    }

}
