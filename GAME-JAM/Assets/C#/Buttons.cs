using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void OnPress()
    {
        SceneManager.LoadScene("Labyrinth_Testing");
    }
    public void ContinueButton()
    {
        PauseMenu.thisPauseMenu.HidePauseMenu();
        Time.timeScale = 1.0f;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void Restart()
    {
        LoopManager.instance.Retry();
        MazeGenerator.Instance.RespawnPlayer();
        PauseMenu.thisPauseMenu.HideDeathMenu();
    }
}
