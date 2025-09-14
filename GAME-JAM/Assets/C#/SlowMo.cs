using UnityEngine;

public class SlowMo : MonoBehaviour
{
    public float slowMotionTimescale;

    private float startTimescale;
    private float startFixedDeltaTime;

    void Start()
    {
        startTimescale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            Debug.Log(PlayerStats.GetPlayerAmmo());
        if (PlayerStats.GetPlayerAmmo() <= 0)
        return;

        if (Input.GetKeyDown(KeyCode.Mouse0))//mouse0 is shoot
        {
            StartSlowMotion();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))//mouse0 is shoot
        {
            StopSlowMotion();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
            StopSlowMotion();
    }

    private void StartSlowMotion()
    {
        Time.timeScale = slowMotionTimescale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimescale;
    }

    private void StopSlowMotion()
    {
        Time.timeScale = startTimescale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }
}