using UnityEngine;

public class SlowMo : MonoBehaviour
{
    public float slowMotionTimescale;

    private float startTimescale;
    private float startFixedDeltaTime;
    static private bool isInSlowMo;
    public static bool disableSlowMo;

    void Start()
    {
        startTimescale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (disableSlowMo)
            { return; } 
        if (PlayerStats.GetIsDead())
        {
            if(isInSlowMo) 
            StopSlowMotion();
            return;
        }
        if (ShotGun.GetIsReloading())
            return;
        if (PlayerStats.GetPlayerAmmo() > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))//mouse0 is shoot
            {
                StartSlowMotion();
                isInSlowMo = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))//mouse0 is shoot
            {
                StopSlowMotion();
                isInSlowMo = false;
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                isInSlowMo = false; 
                StopSlowMotion();
            }

        }
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

    static public bool GetIsInSlowMo()
    {
        return isInSlowMo;
    }
}

