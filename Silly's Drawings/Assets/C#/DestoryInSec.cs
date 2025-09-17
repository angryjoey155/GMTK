using UnityEngine;

public class DestoryInSec : MonoBehaviour
{
    [SerializeField] Cooldown _destoryTimer;

    void Start()
    {
        _destoryTimer.StartCooldown();
    }
    void Update()
    {
        if (!_destoryTimer.IsCoolingDown)
        {
            Destroy(gameObject);
        }
    }
}
