using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScripts : MonoBehaviour
{
    [SerializeField] Movement movementScript;
    [SerializeField] ShotGun shotGunScript;
    [SerializeField] SlowMo SlowMoScript;

    private void Update()
    {
        if (Buttons.isPaused)
        {
            SlowMoScript.enabled = false;
            movementScript.enabled = false;
            shotGunScript.enabled = false;
        }
        else
        {
            SlowMoScript.enabled = true;
            movementScript.enabled = true;
            shotGunScript.enabled = true;
        }
    }
}
