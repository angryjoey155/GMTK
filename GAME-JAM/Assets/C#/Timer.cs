using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI pb;
    float timerValue = 0f;
    private void Update()
    {
        timerValue += Time.deltaTime;
        timer.text = "Timer: " + timerValue .ToString("f2");
        
        if(LoopManager._enemyCounter <= 0)
        {
            OnKillAll();
        }
    }

    void OnKillAll()
    {
        float n = 9999;

        if(timerValue < n)
        {
            pb.text = timerValue.ToString("f2");
            n = timerValue;
        }
        timerValue = 0f;
    }
}
