using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBar : MonoBehaviour
{
    public static ReloadBar instance;
    private void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
    }

    public void ShowGameObject(bool show)
    {
        this.gameObject.SetActive(show);
    }
}
