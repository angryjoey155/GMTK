using System;
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
        if(LoopManager.enemyCounter <= 0)
            return;
        if (PlayerStats.GetPlayerHealth() > 0)
        {
            timer += Time.deltaTime;
            var tspan = TimeSpan.FromSeconds(timer);
            time.text = $"Time: {tspan.Minutes:D2}:{tspan.Seconds:D2}:{tspan.Milliseconds:D2}";
        }
        else
            timer = 0;
    }

    double n = 9999;
    public void OnWin()
    {

        if (timer <  n)
        {
            var tspan = TimeSpan.FromSeconds(timer);
            pb.text = $"{tspan.Minutes:D2}:{tspan.Seconds:D2}:{tspan.Milliseconds:D2}";
            n = timer;
        }
        timer = 0f;
    }
}
