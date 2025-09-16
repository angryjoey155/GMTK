using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] AudioClip _SelectAC;
    public static bool isPaused;

    private void Start()
    {
        isPaused = false;
    }

    public void OnPress()
    {
        AudioSource.PlayClipAtPoint(_SelectAC, transform.position);

        Invoke("GoIntoGame", _SelectAC.length/2);
    }
    void GoIntoGame()
    {
        SceneManager.LoadScene("Labyrinth_Testing");
    }
    public void ContinueButton()
    {
        isPaused = false;
        AudioSource.PlayClipAtPoint(_SelectAC, transform.position);
        PauseMenu.thisPauseMenu.HidePauseScreen();
        Time.timeScale = 1.0f;
    }
    public void ReturnToMainMenu()
    {
        AudioSource.PlayClipAtPoint(_SelectAC, transform.position);
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void RestartButton()
    {
        isPaused = false;
        LoopManager.instance.Restart();
        PauseMenu.thisPauseMenu.HideDeathScreen();
    }
}
