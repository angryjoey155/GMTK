using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI pb;

    float timer;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (PlayerStats.GetPlayerHealth() > 0)
        {
            timer += Time.deltaTime;

            time.text = "Time: " + timer.ToString("f2");
        }
        else
            timer = 0;
    }

    public void OnWin()
    {
        float n = 9999;

        if (timer <  n)
        {
            pb.text = timer.ToString();
            n = timer;
            timer = 0f;
        }
    }
}
