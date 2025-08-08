using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] GameObject _projectile;
    [SerializeField] Transform _spawnLoc;
    [SerializeField] Cooldown _timeBetweenShots;
    [SerializeField] LayerMask LayerMask;

    [SerializeField] List<Sprite> _RunningFrame = new List<Sprite>();
    [SerializeField] List<Sprite> _IdleFrame = new List<Sprite>();
    [SerializeField] List<Sprite> _HandGunFrame = new List<Sprite>();
    [SerializeField] Animator _handAnimator;
    [SerializeField] Transform ShootPoint;

    private bool isAttacking;
    private bool isFacingRight = true;
    private Rigidbody2D rb;
    private float maxRange = 1;
    private float minRange = -1;
    private Animator Animator; 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _spawnLoc.SetParent(null);
        Animator = GetComponent<Animator>();
        _handAnimator.SetNewFrames(null);
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

       RaycastHit2D ray2D =  Physics2D.Raycast(transform.position, direction, distance, LayerMask);
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
        if (isAttacking)        //In Attacking State
        {
            Vector2 Direction = Movement.player.gameObject.transform.position - transform.position;
            Direction.y = 0;
            Direction.Normalize();
            rb.velocity = new Vector2(Direction.x * _speed, 0);

            if (!_timeBetweenShots.IsCoolingDown)
            {
                GameObject Fish = Instantiate(_projectile, ShootPoint.position, Quaternion.identity);
                Fish.transform.localScale = new Vector3(0.5f,0.5f,1);
                _timeBetweenShots.StartCooldown();
            }
        }
        else                    //In Idle State
        {

            Vector2 Direction = _spawnLoc.position - transform.position;
            Direction.y = 0;
            Direction.Normalize();
            if (Mathf.Abs(_spawnLoc.position.x - transform.position.x) >= 0.1f)
                rb.velocity = new Vector2(Direction.x * _speed, 0);
        }
        if (rb.velocity.x > 0.05f || rb.velocity.x < -0.05f )       //is moving 
        {
            if (Animator.frames[0] != _RunningFrame[0])
            {
                Animator.SetNewFrames(_RunningFrame);
                _handAnimator.SetNewFrames(_HandGunFrame);
            }
        }
        else if( Animator.frames[0] != _IdleFrame[0])               //Standing still
        {
            Animator.SetNewFrames(_IdleFrame);
            _handAnimator.SetNewFrames(null);
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
