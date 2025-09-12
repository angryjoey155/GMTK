using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    [SerializeField] GameObject _projectile;
    [SerializeField] Cooldown _timeBetweenShots;
    [SerializeField] LayerMask LayerMask;
    [SerializeField] GameObject Gun;
    [SerializeField] Transform ShotLoc;
    [SerializeField] AudioClip ShootAC;

    private bool isAttacking;
    private bool isFacingRight = true;
    private float maxRange = 1;
    private float minRange = -1;


    // Update is called once per frame
    void Update()
    {
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
            //Gun.transform.LookAt(Movement.player.transform.position);
            if (!_timeBetweenShots.IsCoolingDown)
            {
                Instantiate(_projectile, ShotLoc.position, Quaternion.identity);
                _timeBetweenShots.StartCooldown();
                AudioSource.PlayClipAtPoint(ShootAC, transform.position);
            }
        }
        else                    //In Idle State
        {
            
            Vector2 Direction = transform.position - transform.position;
            Direction.y = 0;
            Direction.Normalize();
        }
    }

}
