using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] GameObject _projectile;
    [SerializeField] Transform _spawnLoc;
    [SerializeField] Cooldown _timeBetweenShots;
    [SerializeField] LayerMask LayerMask;

    [SerializeField] Animator _handAnimator;
    [SerializeField] Transform ShootPoint;

    private bool isFacingRight = true;
    private Rigidbody2D rb;
    private Animator Animator;
    bool isAttacking;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            rb = GetComponent<Rigidbody2D>();
            _spawnLoc.SetParent(null);
            Animator = GetComponent<Animator>();
            _handAnimator.SetNewFrames(null);
        }
        catch (Exception e) { }
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }
    private void FixedUpdate()
    {
        CheckState();
        EnemyBehavior();
    }

    private void CheckState()
    {
        Vector3 direction = Movement.player.transform.position - transform.position;
        float distance = 10f;
        direction.z = 0;
        direction.Normalize();

        RaycastHit2D ray2D = Physics2D.Raycast(transform.position, direction, distance, LayerMask);
        if (ray2D)
        {
            if (ray2D.collider.CompareTag("Player"))
                isAttacking = true;
            else
                isAttacking = false;
        }
    }


    private void EnemyBehavior()
    {

            Vector2 Direction = Movement.player.gameObject.transform.position - transform.position;
            Direction.Normalize();
            rb.velocity = Direction * _speed;

            if (!_timeBetweenShots.IsCoolingDown)
            {
                Vector2 vector2 = ShootPoint.position;
                vector2.y = vector2.y + .5f;

                Instantiate(_projectile, vector2, Quaternion.identity);
                _timeBetweenShots.StartCooldown();
            }
    }

    private void Flip()
    {
        if (isFacingRight && rb.velocity.x < 0f || !isFacingRight && rb.velocity.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

