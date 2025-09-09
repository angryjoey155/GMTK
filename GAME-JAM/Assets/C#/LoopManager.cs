using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    static int _enemyCounter;
    int _maxCounter;
    GameObject[] _enemyList;

    static public void ChangeEnemyCounter(int amount)
    {
        _enemyCounter += amount;
    }
    // Start is called before the first frame update
    void Start()
    {
        _enemyCounter = GameObject.FindGameObjectsWithTag("enemy").Length;
        _maxCounter = _enemyCounter;
        _enemyList = new GameObject[_maxCounter];
        _enemyList = GameObject.FindGameObjectsWithTag("enemy");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_enemyCounter <= 0)
        {
            _enemyCounter = _maxCounter;
            foreach (var enemy in _enemyList) {
                Instantiate(enemy, enemy.transform.position, Quaternion.identity);
            }
        }
    }
}
