using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public static LoopManager instance;

    public static int enemyCounter;
    public static bool roundOver = false;
    int _maxCounter;
    GameObject[] _enemyList;
    Vector3[] _enemyLoc;
    int[] _enemyType;
    Vector3 _playerStartPos;

    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private static Cooldown _timeBetweenRounds = new Cooldown();
    [SerializeField] private AudioClip _countdownAC;
    [SerializeField] private AudioClip _finalHitAC;
    [SerializeField] private GameObject _killZone;
    [SerializeField] public static AudioSource countdownAudioSource;
    static public void ChangeEnemyCounter(int amount)
    {
        enemyCounter += amount;
        _timeBetweenRounds.StartCooldown();
    }
    private void Awake()
    {
        countdownAudioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _playerStartPos = Movement.player.gameObject.transform.position;

        _timeBetweenRounds.cooldownTime = 3f;
        _maxCounter = GameObject.FindGameObjectsWithTag("enemy").Length;
        enemyCounter = _maxCounter;
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
        if(enemyCounter <= 0 && !roundOver)
        {
            roundOver = true;
            Timer.instance.OnWin();
        }
        if (enemyCounter <= 0)
        {
            DestroyProjectiles();
            if (readyToPlay)
            {
                SlowMo.disableSlowMo = true;
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = 0.02f;
                countdownAudioSource.Play();
                AudioSource.PlayClipAtPoint(_finalHitAC, transform.position);
                readyToPlay = false;
            }
            if (!_timeBetweenRounds.IsCoolingDown)
            {
                SlowMo.disableSlowMo = false;
                enemyCounter = _maxCounter;
                PLaceGuys();
                readyToPlay = true;
                roundOver = false;
            }
        }
    }
    void DestroyProjectiles()
    {
        GameObject[] list = new GameObject[GameObject.FindGameObjectsWithTag("Projectile").Length];
        list = GameObject.FindGameObjectsWithTag("Projectile");

        for (int i = 0; i < list.Length; i++)
        {
            Destroy(list[i]);
        }
    }
    bool readyToPlay = true;
    private void PLaceGuys()
    {
        for (int i = 0; i < _maxCounter; i++)
        {
            Instantiate(_enemies[_enemyType[i]], _enemyLoc[i], Quaternion.identity);
        }
    }
    public void Restart()
    {
        List<GameObject> list = new List<GameObject>(GameObject.FindGameObjectsWithTag("enemy"));

        for(int i = 0; i < list.Count; i++)
        {
            Destroy(list[i]);
        }
        ChangeEnemyCounter(_maxCounter);
        PLaceGuys();

        Movement.player.gameObject.transform.position = _playerStartPos;
        PlayerStats.ChangeAmmo(3);
        PlayerStats.ChangeHealth(4);

        Instantiate(_killZone,Movement.player.transform);
        Invoke("AddOneAmmo", 0.005f);

        DestroyProjectiles();
    }
    void AddOneAmmo()
    {
        PlayerStats.ChangeAmmo(1);
    }
}
