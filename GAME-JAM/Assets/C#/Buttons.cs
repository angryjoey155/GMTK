using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] AudioClip _SelectAC;
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
        LoopManager.instance.Restart();
        PauseMenu.thisPauseMenu.HideDeathScreen();
    }
}
