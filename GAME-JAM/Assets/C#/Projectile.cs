using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _ProjectileSpeed;
    [SerializeField] int _damage = -1;
    Vector3 _direction;
    void Start()
    {
        _direction = Movement.player.transform.position - transform.position;
        _direction.z = 0;
        _direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_direction);
        transform.position += _direction * _ProjectileSpeed * Time.deltaTime;
        transform.up = _direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats.ChangeHealth(_damage);
            Destroy(gameObject);
        }
    }
}
