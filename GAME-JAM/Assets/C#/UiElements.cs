using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiElements : MonoBehaviour
{
    [SerializeField] Image[] totalHealth;
    [SerializeField] Image[] totalAmmo;   
    [SerializeField] Sprite currentHealth;
    [SerializeField] Sprite currentAmmo;

    void Start()
    {
        for(int i = totalHealth.Length; i > PlayerStats.GetPlayerHealth(); i--)
        {
            totalHealth[i - 1].sprite = currentHealth;
        }
        for (int i = totalAmmo.Length; i > PlayerStats.GetPlayerAmmo(); i--)
        {
            totalAmmo[i - 1].sprite = currentAmmo;
        }
    }
}
