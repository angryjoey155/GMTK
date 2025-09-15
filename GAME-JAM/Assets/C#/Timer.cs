using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI pb;

    double timer;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (PlayerStats.GetPlayerHealth() > 0)
        {
            timer += Time.deltaTime;

            time.text = "Time: " + TimeSpan.FromSeconds(timer).ToString("mm:ss");
        }
        else
            timer = 0;
    }

    double n = 9999;
    public void OnWin()
    {

        if (timer <  n)
        {
            pb.text = TimeSpan.FromSeconds(timer).ToString("mm:ss");
            n = timer;
            timer = 0f;
        }
    }
}
