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


    private bool isAttacking;
    private bool canSwapFrames;
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
                Instantiate(_projectile, transform.position, Quaternion.identity);
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
        if (rb.velocity.x > 0.05f || rb.velocity.x < -0.05f) //is moving 
        {
            Debug.Log("1");
            Animator.SetNewFrames(_RunningFrame);
            canSwapFrames = false;
        }
        else                     //Standing still
        {
            Debug.Log("2");
            Animator.SetNewFrames(_IdleFrame);
            canSwapFrames = false;
        }
    }

    private void CheckNewState(List<Sprite> Frames)
    {
        if (Animator.frames[0] == Frames[0])
        {
            Debug.Log("3");

            canSwapFrames = true;
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
