using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject PauseButton;

    public static string previousScene;

    // Start is called before the first frame update
    public void Start()
    {
        PauseMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;

        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;

        PauseButton.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void GoToSettings()
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Settings");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
