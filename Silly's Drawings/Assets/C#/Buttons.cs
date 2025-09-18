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
        AudioSource.PlayClipAtPoint(_SelectAC, new Vector3(0,0,0),1f);
        Invoke("GoIntoGame", _SelectAC.length/2);
    }
    public void ResetLevel()
    {
        Time.timeScale = 1.0f;
        AudioSource.PlayClipAtPoint(_SelectAC, new Vector3(0, 0, 0));
        SceneManager.LoadScene("Labyrinth_Testing");
    }
    public void GoIntoGame()
    {
        SceneManager.LoadScene("Labyrinth_Testing");
        SceneManager.UnloadSceneAsync(0);
    }
    public void ContinueButton()
    {
        LoopManager.countdownAudioSource.Play();
        isPaused = false;
        AudioSource.PlayClipAtPoint(_SelectAC, new Vector3(0, 0, 0));
        PauseMenu.thisPauseMenu.HidePauseScreen();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1.0f;
        AudioSource.PlayClipAtPoint(_SelectAC, new Vector3(0, 0, 0));
        SceneManager.LoadScene("MainMenu");
        SceneManager.UnloadSceneAsync(1);
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
