using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public PauseMenuScript pauseMenuScript;

    // This method will be linked to the "New Game" button
    public void NewGame()
    {
        Debug.Log("New Game button clicked!");
        // Load the first level or main gameplay scene
        SceneManager.LoadScene("LevelOne");
    }

    // This method will be linked to the "Load Game" button
    public void LoadGame()
    {
        Debug.Log("Load Game button clicked!");
        // Load the saved game data
        
    }

    // This method will be linked to the "Exit" button
    public void ExitGame()
    {
        Debug.Log("Exit button clicked!");
        // Exit the game
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void EndGame()
    {
        Debug.Log("End Game!");
        // Load the first level or main gameplay scene
        SceneManager.LoadScene("EndGame");
    }
}
