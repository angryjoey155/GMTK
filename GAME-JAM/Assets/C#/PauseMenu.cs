using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathScreen;

    public static PauseMenu thisPauseMenu;

    private void Start()
    {
        thisPauseMenu = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void DeathScreen()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    public void HideDeathScreen()
    {
        PlayerStats.ChangeHealth(3);
        deathScreen.SetActive(false);
        Time.timeScale = 1f;
    }
}
