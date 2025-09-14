using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public static LoopManager instance;

    public static int _enemyCounter;
    int _maxCounter;
    GameObject[] _enemyList;
    Vector3[] _enemyLoc;
    int[] _enemyType;

    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private static Cooldown _timeBetweenRounds = new Cooldown();
    [SerializeField] private AudioClip _countdownAC;
    [SerializeField] private AudioClip _finalHitAC;
    static public void ChangeEnemyCounter(int amount)
    {
        _enemyCounter += amount;
        _timeBetweenRounds.StartCooldown();
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        _timeBetweenRounds.cooldownTime = 3f;
        _maxCounter = GameObject.FindGameObjectsWithTag("enemy").Length;
        _enemyCounter = _maxCounter;
        _enemyList = new GameObject[_maxCounter];
        _enemyLoc = new Vector3[_maxCounter];
        _enemyType = new int[_maxCounter];
        _enemyList = GameObject.FindGameObjectsWithTag("enemy");
        for(int i = 0;  i < _enemyList.Length; i++)
        {
            _enemyLoc[i] = _enemyList[i].transform.position;
            _enemyType[i] = _enemyList[i].GetComponent<LoopSetter>().type;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_enemyCounter <= 0)
        {
            if (readyToPlay)
            {
                AudioSource.PlayClipAtPoint(_countdownAC, transform.position);
                AudioSource.PlayClipAtPoint(_finalHitAC, transform.position);
                readyToPlay = false;
            }
            if (!_timeBetweenRounds.IsCoolingDown)
            {
                _enemyCounter = _maxCounter;
                PLaceGuys();
                readyToPlay = true;
            }
        }
    }
    bool readyToPlay = true;
    private void PLaceGuys()
    {
        for (int i = 0; i < _maxCounter; i++)
        {
            Instantiate(_enemies[_enemyType[i]], _enemyLoc[i], Quaternion.identity);
        }
        //_enemyList = GameObject.FindGameObjectsWithTag("enemy");
    }

    public void Retry()
    {
        GameObject[] list = new GameObject[GameObject.FindGameObjectsWithTag("enemy").Length];
        list = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < list.Length;i++)
        {
            Debug.Log(list[i]);
            Destroy(list[i]);
        }
        //PLaceGuys();
    }
}
