using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI pb;

    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        time.text = "Time: " + timer;
    }

    void OnWin()
    {
        float n = 999;
    }
}
