using UnityEngine;

public class LoopSetter : MonoBehaviour
{
    [SerializeField] public int type;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDestroy()
    {
        LoopManager.ChangeEnemyCounter(-1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
