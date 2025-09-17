using TMPro;
using UnityEngine;

public class LapTimer : MonoBehaviour
{
    float curTime = 0f;
    [SerializeField] TextMeshPro Timer;
    [SerializeField] TextMeshPro BestTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

    }
}
